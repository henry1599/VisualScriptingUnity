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

        public static byte[] EncodeTexture2D( Texture2D texture )
        {
            byte[] textureBytes = texture.EncodeToPNG();
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes( Header );
            byte[] result = new byte[ headerBytes.Length + textureBytes.Length ];

            System.Buffer.BlockCopy( headerBytes, 0, result, 0, headerBytes.Length );
            System.Buffer.BlockCopy( textureBytes, 0, result, headerBytes.Length, textureBytes.Length );

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

            byte[] textureBytes = new byte[ fileBytes.Length - headerLength ];
            System.Buffer.BlockCopy( fileBytes, headerLength, textureBytes, 0, textureBytes.Length );

            Texture2D texture = new Texture2D( 2, 2 );
            texture.LoadImage( textureBytes );
            texture.filterMode = FilterMode.Point;
            return texture;
        }
    }
}
