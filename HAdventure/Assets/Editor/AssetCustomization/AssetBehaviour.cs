using UnityEngine;
using UnityEditor;
using Baram.Utils.EditorTools;
using System;
using Object = UnityEngine.Object;
using System.Linq;

[InitializeOnLoad]
public class AssetBehaviour
{
    static AssetBehaviour()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
    }

    string warningFolder = string.Empty;

    private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);

        if (AssetDatabase.IsValidFolder(path))
        {
            AssetCustomizeData data = AssetCustomizeData.Create(path, selectionRect);
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && selectionRect.Contains(Event.current.mousePosition))
            {
                if (!data.NeedShowWarningOnOpen)
                {
                    var windows = Resources.FindObjectsOfTypeAll<FolderWarningWindow>();
                    foreach (var window in windows)
                    {
                        window.Close();
                    }
                    return;
                }
                Event.current.Use();  // Prevent default selection
                Vector2 mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition) + Vector2.right * 20;
                FolderWarningWindow.ShowWindow(path, mousePosition, data.WarningMessage);
            }
        }
    }

    public class FolderWarningWindow : EditorWindow
    {
        private static string folderPath;
        private static string warningMessage;
        private GUIStyle titleStyle;
        private GUIStyle contentStyle;

        public static void ShowWindow(string path, Vector2 position, string message = "")
        {
            folderPath = path;
            warningMessage = string.IsNullOrEmpty(message) ? AssetCustomizerMeta.DefaultWarningMessage : message;// Find any existing instance of FolderWarningWindow
            FolderWarningWindow window = Resources.FindObjectsOfTypeAll<FolderWarningWindow>().FirstOrDefault();
            if (window == null)
            {
                window = CreateInstance<FolderWarningWindow>();
                window.position = new Rect(position.x, position.y, 300, 160);
                window.ShowPopup();
            }
            else
            {
                window.position = new Rect(position.x, position.y, 300, 160);
                window.OnDisable();
                window.OnEnable();
            }
        }
        public void OnEnable() 
        {
            try
            {
                this.titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 14,
                    fontStyle = FontStyle.Bold,
                };
                this.contentStyle = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter, 
                    fontSize = 12,
                    fontStyle = FontStyle.Normal,
                    padding = new RectOffset(2, 2, 2, 2),
                    wordWrap = true,
                };// Subscribe to the beforeAssemblyReload event
                AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            }
            catch (Exception)
            {
                this.titleStyle = EditorStyles.boldLabel;
                this.contentStyle = EditorStyles.label;
            }
        }
        public void OnDisable()
        {
            // Unsubscribe from the beforeAssemblyReload event
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
        }

        private void OnBeforeAssemblyReload()
        {
            // Close the window before assembly reload
            Close();
        }

        private void OnGUI()
        {
            // Set custom background color
            Color backgroundColor = new Color(40f / 255f, 40f / 255f, 40f / 255f);
            Color outlineColor = new Color(255f / 255f, 253f / 255f, 111f / 255f);

            // Create a custom GUIStyle for the box
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(5, 5, 5, 5),
                normal = { background = Common.MakeTex2D(2, 2, backgroundColor) }
            };
            GUIStyle outlineStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(2, 2, 2, 2),
                normal = { background = Common.MakeTex2D(2, 2, outlineColor) }
            };
            GUILayout.BeginVertical(outlineStyle);
                GUILayout.BeginVertical(boxStyle);
                    var warningIcon = EditorGUIUtility.IconContent("console.warnicon");
                    EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(warningIcon, GUILayout.Width(20), GUILayout.Height(20));
                        EditorGUILayout.BeginVertical();
                            EditorGUILayout.LabelField("Warning", this.titleStyle);
                            EditorGUILayout.LabelField($"Folder: {folderPath}", this.contentStyle);
                        EditorGUILayout.EndVertical();
                        GUILayout.Label(warningIcon, GUILayout.Width(20), GUILayout.Height(20));
                        GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField(warningMessage, this.contentStyle, GUILayout.MaxHeight(60), GUILayout.ExpandHeight(true));
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Open"))
                    {
                        RevealFirstItemInFolder(folderPath);
                        Close();
                    }
                    if (GUILayout.Button("Cancel"))
                    {
                        Close();
                    }
                    GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        // * Cheat to ping the very first item of the folder
        // * so that unity editor can reveal the whole folder
        private void RevealFirstItemInFolder(string path)
        {
            string[] assetGuids = AssetDatabase.FindAssets("", new[] { path });

            if (assetGuids.Length > 0)
            {
                string firstAssetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
                Object firstAsset = AssetDatabase.LoadAssetAtPath<Object>(firstAssetPath);

                Selection.activeObject = firstAsset;
                EditorGUIUtility.PingObject(firstAsset);
            }
            else
            {
                Object folderObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                Selection.activeObject = folderObject;
                EditorGUIUtility.PingObject(folderObject);
            }
        }
    }
}
