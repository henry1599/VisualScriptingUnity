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
            GUILayout.Label( "Select Folder to Encode PNG Files", EditorStyles.boldLabel );

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
    }
}
