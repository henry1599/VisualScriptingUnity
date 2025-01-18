using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CharacterStudio.Editor
{
    public class CSIViewer : EditorWindow
    {
        private string folderPath = string.Empty;
        private List<Texture2D> decodedTextures = new List<Texture2D>();
        private string selectedTexturePath = string.Empty;
        private Texture2D selectedTexture = null;
        private Vector2 scrollPosition;
        private float itemSize = 64f;

        [MenuItem( "Tools/CSI Viewer" )]
        public static void ShowWindow()
        {
            GetWindow<CSIViewer>( "CSI Viewer" );
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            folderPath = EditorGUILayout.TextField( "Folder Path", folderPath, GUILayout.ExpandWidth( true ) );
            if ( GUILayout.Button( "Browse", GUILayout.ExpandWidth( false ) ) )
            {
                string path = EditorUtility.OpenFolderPanel( "Select Folder", "", "" );
                if ( !string.IsNullOrEmpty( path ) )
                {
                    folderPath = path;
                }
            }
            EditorGUILayout.EndHorizontal();

            if ( !string.IsNullOrEmpty( folderPath ) )
            {
                if ( GUILayout.Button( "Inspect" ) )
                {
                    InspectFolder();
                }
            }

            if ( decodedTextures.Count > 0 )
            {
                EditorGUILayout.LabelField( "Decoded Textures:" );
                itemSize = EditorGUILayout.Slider( "Item Size", itemSize, 32f, 200f );
                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition );
                float windowWidth = position.width;
                int columns = Mathf.FloorToInt( windowWidth / ( itemSize + 6 ) ); // itemSize for texture + 6 for padding
                int rows = Mathf.CeilToInt( decodedTextures.Count / ( float ) columns );
                for ( int row = 0; row < rows; row++ )
                {
                    EditorGUILayout.BeginHorizontal();
                    for ( int col = 0; col < columns; col++ )
                    {
                        int index = row * columns + col;
                        if ( index < decodedTextures.Count )
                        {
                            if ( GUILayout.Button( decodedTextures[ index ], GUILayout.Width( itemSize ), GUILayout.Height( itemSize ) ) )
                            {
                                selectedTexturePath = GetTexturePath( index );
                                selectedTexture = decodedTextures[ index ];
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }

            GUILayout.FlexibleSpace();

            if ( !string.IsNullOrEmpty( selectedTexturePath ) )
            {
                EditorGUILayout.LabelField( "Selected Texture Path:", selectedTexturePath );
                EditorGUILayout.LabelField( "Texture Size:", $"{selectedTexture.width}x{selectedTexture.height}" );
            }
        }

        private void InspectFolder()
        {
            decodedTextures.Clear();
            string[] csiFiles = Directory.GetFiles( folderPath, "*.csi", SearchOption.AllDirectories );
            foreach ( string csiFile in csiFiles )
            {
                Texture2D texture = CSIFile.LoadCsiFile( csiFile );
                if ( texture != null )
                {
                    decodedTextures.Add( texture );
                }
            }
        }

        private string GetTexturePath( int index )
        {
            string[] csiFiles = Directory.GetFiles( folderPath, "*.csi", SearchOption.AllDirectories );
            if ( index >= 0 && index < csiFiles.Length )
            {
                return csiFiles[ index ];
            }
            return string.Empty;
        }
    }
}
