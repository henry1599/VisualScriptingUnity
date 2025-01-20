using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace CharacterStudio
{
    public class CSIFile
    {
        private static readonly string Header = "Ch4ract3r5tud10";
        private static readonly int IsDefault = 0;

        public static byte[] EncodeTexture2D( Texture2D texture )
        {
            byte[] textureBytes = texture.EncodeToPNG();
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes( Header );
            byte[] isDefaultBytes = System.BitConverter.GetBytes( IsDefault );
            byte[] result = new byte[ headerBytes.Length + isDefaultBytes.Length + textureBytes.Length ];

            System.Buffer.BlockCopy( headerBytes, 0, result, 0, headerBytes.Length );
            System.Buffer.BlockCopy( isDefaultBytes, 0, result, headerBytes.Length, isDefaultBytes.Length );
            System.Buffer.BlockCopy( textureBytes, 0, result, headerBytes.Length + isDefaultBytes.Length, textureBytes.Length );

            return result;
        }

        public static void SaveAsCsiFile( Texture2D texture, string outputPath )
        {
            byte[] encodedData = EncodeTexture2D( texture );
            File.WriteAllBytes( outputPath, encodedData );
        }

        public static Texture2D LoadCsiFile( string filePath )
        {
            byte[] fileBytes = File.ReadAllBytes( filePath );
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes( Header );
            int headerLength = headerBytes.Length;
            int isDefaultLength = sizeof( int );

            byte[] isDefaultBytes = new byte[ isDefaultLength ];
            System.Buffer.BlockCopy( fileBytes, headerLength, isDefaultBytes, 0, isDefaultLength );
            int isDefault = System.BitConverter.ToInt32( isDefaultBytes, 0 );

            byte[] textureBytes = new byte[ fileBytes.Length - headerLength - isDefaultLength ];
            System.Buffer.BlockCopy( fileBytes, headerLength + isDefaultLength, textureBytes, 0, textureBytes.Length );

            Texture2D texture = new Texture2D( 2, 2, TextureFormat.RGBA32, false );
            texture.LoadImage( textureBytes );
            texture.filterMode = FilterMode.Point;
            return texture;
        }
    }
}
