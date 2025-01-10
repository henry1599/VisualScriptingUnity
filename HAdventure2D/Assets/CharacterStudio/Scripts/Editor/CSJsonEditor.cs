using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using System.Linq;

namespace CharacterStudio.Editor
{
    public class CSJsonEditor : EditorWindow
    {
        private bool isPrettyPrint = false;
        private string filePath = "";
        private Type selectedType;
        private string[] typeNames;
        private string[] fullyQualifiedTypeNames;
        private int selectedIndex = 0;
        private object selectedInstance;

        [MenuItem( "Window/Json Data Editor" )]
        static void Init()
        {
            GetWindow( typeof( CSJsonEditor ) ).Show();
        }

        private void OnEnable()
        {
            // Find all types that inherit from CSJson
            var types = Assembly.GetAssembly( typeof( CSJson ) ).GetTypes()
                .Where( t => t.IsSubclassOf( typeof( CSJson ) ) && !t.IsAbstract )
                .ToArray();

            typeNames = types.Select( t => t.Name ).ToArray();
            fullyQualifiedTypeNames = types.Select( t => t.AssemblyQualifiedName ).ToArray();
        }

        private void OnGUI()
        {
            #region Select CSJsonable Type

            GUILayout.Label( "Select CSJsonable Type:" );
            selectedIndex = EditorGUILayout.Popup( selectedIndex, typeNames );
            selectedType = Type.GetType( fullyQualifiedTypeNames[ selectedIndex ] );

            #endregion

            #region LoadData Button & CreateData Button & PrettyPrint Toggle & SaveData Button

            EditorGUILayout.Space();

            if ( GUILayout.Button( "Load json data" ) )
                LoadJsonData();

            EditorGUILayout.Space();

            if ( GUILayout.Button( "Create json data" ) )
                CreateNewData();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            isPrettyPrint = GUILayout.Toggle( isPrettyPrint, "Pretty Print" );

            if ( GUILayout.Button( "Save data" ) )
                SaveJsonData();

            GUILayout.EndHorizontal();

            #endregion

            EditorGUILayout.Space();

            #region Handle Data Box

            GUILayout.BeginVertical( "Box" );
            if ( selectedInstance != null )
            {
                GUILayout.Label( filePath );
                SerializedObject serializedObject = new SerializedObject( this );

                // Get fields of the selected type
                var fields = selectedType.GetFields( BindingFlags.Public | BindingFlags.Instance );
                foreach ( var field in fields )
                {
                    var fieldValue = field.GetValue( selectedInstance );
                    if ( fieldValue != null )
                    {
                        EditorGUILayout.LabelField( field.Name, fieldValue.ToString() );
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();

            #endregion

            EditorGUILayout.Space();
        }

        private void LoadJsonData()
        {
            filePath = EditorUtility.OpenFilePanel( "Select json data file", Application.streamingAssetsPath, "json" );

            if ( !string.IsNullOrEmpty( filePath ) )
            {
                string dataAsJson = File.ReadAllText( filePath );
                selectedInstance = JsonUtility.FromJson( dataAsJson, selectedType );
            }
        }

        private void SaveJsonData()
        {
            filePath = EditorUtility.SaveFilePanel( "Save json data file", Application.streamingAssetsPath, "", "json" );

            if ( !string.IsNullOrEmpty( filePath ) )
            {
                string dataAsJson = JsonUtility.ToJson( selectedInstance, isPrettyPrint );
                File.WriteAllText( filePath, dataAsJson );
            }
        }

        private void CreateNewData()
        {
            filePath = "";
            selectedInstance = Activator.CreateInstance( selectedType );
        }
    }
}