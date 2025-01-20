using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CharacterStudio.Editor
{
    public class CSIEncodeEditor : EditorWindow
    {
        private string folderPath = "";

        [MenuItem( "Tools/CSI Encode Editor" )]
        public static void ShowWindow()
        {
            GetWindow<CSIEncodeEditor>( "CSI Encode Editor" );
        }

        private void OnGUI()
        {
            GUILayout.Label( "Select Folder to Encode/Decode Files", EditorStyles.boldLabel );

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField( "Selected Folder: ", folderPath, GUILayout.ExpandWidth( true ) );
            if ( GUILayout.Button( "Browse", GUILayout.ExpandWidth( false ) ) )
            {
                folderPath = EditorUtility.OpenFolderPanel( "Select Folder", "", "" );
            }
            EditorGUILayout.EndHorizontal();

            if ( !string.IsNullOrEmpty( folderPath ) )
            {
                if ( GUILayout.Button( "Encode PNG Files" ) )
                {
                    EncodePngFiles( folderPath );
                }

                if ( GUILayout.Button( "Decode CSI Files" ) )
                {
                    DecodeCsiFiles( folderPath );
                }

                if (GUILayout.Button("Remove PNG Files"))
                {
                    RemovePngs(folderPath);
                }

                if (GUILayout.Button("Remove CSI Files"))
                {
                    RemoveCsis(folderPath);
                }
            }
        }

        private void EncodePngFiles( string path )
        {
            string[] files = Directory.GetFiles( path, "*.png", SearchOption.AllDirectories );

            foreach ( string file in files )
            {
                string csiFilePath = Path.ChangeExtension( file, ".csi" );
                byte[] pngData = File.ReadAllBytes( file );
                Texture2D texture = new Texture2D( 2, 2 );
                texture.LoadImage( pngData );
                CSIFile.SaveAsCsiFile( texture, csiFilePath );
            }

            EditorUtility.DisplayDialog( "Encoding Complete", "All PNG files have been encoded to CSI format.", "OK" );
        }
        private void RemovePngs(string path)
        {
            string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
        private void RemoveCsis(string path)
        {
            string[] files = Directory.GetFiles(path, "*.csi", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
        private void DecodeCsiFiles( string path )
        {
            string[] files = Directory.GetFiles( path, "*.csi", SearchOption.AllDirectories );

            foreach ( string file in files )
            {
                CSIFileData data = CSIFile.LoadCsiFile( file );
                if ( data.Texture != null )
                {
                    byte[] pngData = data.Texture.EncodeToPNG();
                    string pngFilePath = Path.ChangeExtension( file, ".png" );
                    File.WriteAllBytes( pngFilePath, pngData );
                }
            }

            EditorUtility.DisplayDialog( "Decoding Complete", "All CSI files have been decoded to PNG format.", "OK" );
        }
    }
}
