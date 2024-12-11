using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Baram.Game.Editor
{
    public class ExcludeFromBuildEditor : EditorWindow
    {
        private static HashSet<string> cacheData = new HashSet<string>();
        private ReorderableList reorderableList;
        private GUIStyle titleStyle;
        private List<string> stringList = new List<string>();
        private AssetExcludeFromBuildData assetExcludeFromBuildData;

        [MenuItem("Window/Exclude From Build Editor")]
        public static void ShowWindow()
        {
            GetWindow<ExcludeFromBuildEditor>("Exclude From Build Editor");
        }

        private void OnEnable()
        {
            this.assetExcludeFromBuildData = AssetDatabase.LoadAssetAtPath<AssetExcludeFromBuildData>(ExcludeFoldersFromBuild.DATA_PATH);
            if (assetExcludeFromBuildData != null)
            {
                stringList = assetExcludeFromBuildData.ExcludeFromBuild;
            }
            else
            {
                assetExcludeFromBuildData = ScriptableObject.CreateInstance<AssetExcludeFromBuildData>();
                AssetDatabase.CreateAsset(assetExcludeFromBuildData, ExcludeFoldersFromBuild.DATA_PATH);
                AssetDatabase.SaveAssetIfDirty(assetExcludeFromBuildData);
                AssetDatabase.Refresh();
            }
            cacheData = new HashSet<string>(stringList);
            reorderableList = new ReorderableList(
                elements: stringList,
                elementType: typeof(string),
                draggable: true,
                displayHeader: true,
                displayAddButton: true,
                displayRemoveButton: true
            );

            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "");
            };

            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float buttonWidth = 0f;
                float pingButtonWidth = 30f;
                float textFieldWidth = rect.width - buttonWidth - pingButtonWidth - 15f;

                EditorGUI.LabelField(
                    new Rect(rect.x, rect.y, textFieldWidth, EditorGUIUtility.singleLineHeight),
                    stringList[index]
                );

                if (!string.IsNullOrEmpty(stringList[index]) &&
                    GUI.Button(
                        new Rect(rect.x + textFieldWidth + buttonWidth + 10f, rect.y, pingButtonWidth, EditorGUIUtility.singleLineHeight),
                        EditorGUIUtility.IconContent("sv_icon_dot4_pix16_gizmo")
                        )
                    )
                {
                    PingPath(stringList[index]);
                }
            };

            reorderableList.onAddCallback = (ReorderableList list) =>
            {
                OnAddItem(list.index);
            };

            reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                OnRemoveItem(list.index);
            };
        }
        private void OnAddItem(int index)
        {
            ShowContextMenu(index);
        }
        private void OnRemoveItem(int index)
        {
            if (index < 0 || index >= stringList.Count)
            {
                return;
            }
            string path = stringList[index];
            stringList.RemoveAt(index);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            if (!IsValidPath(path))
            {
                return;
            }
            RemoveFromDataConfig(path);
            AssetCustomizerMeta.SetFolderMetadata(path, AssetCustomizerMeta.CustomIsExcludedFromBuild, "0");
            AssetDatabase.Refresh();
        }

        private void ShowContextMenu(int index)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Folder"), false, () => BrowseFolder(index));
            menu.AddItem(new GUIContent("File"), false, () => BrowseFile(index));
            menu.ShowAsContext();
        }

        private bool IsValidPath(string path)
        {
            return !string.IsNullOrEmpty(path) && (System.IO.File.Exists(path) || System.IO.Directory.Exists(path)) && path.StartsWith("Assets");
        }

        private void BrowseFolder(int index)
        {
            string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(folderPath))
            {
                string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);
                if (this.stringList.Contains(relativePath))
                {
                    return;
                }
                stringList.Add(relativePath);
                AddToDataConfig(relativePath);
            }
            Validate();
        }
        private void BrowseFile(int index)
        {
            string filePath = EditorUtility.OpenFilePanel("Select File", "Assets", "");
            if (!string.IsNullOrEmpty(filePath))
            {
                string relativePath = "Assets" + filePath.Substring(Application.dataPath.Length);
                if (this.stringList.Contains(relativePath))
                {
                    return;
                }
                stringList.Add(relativePath);
                AddToDataConfig(relativePath);
            }
            Validate();
        }
        void AddToDataConfig(string path)
        {
            if (this.assetExcludeFromBuildData == null)
            {
                return;
            }
            cacheData.Add(path);
            if (this.assetExcludeFromBuildData.ExcludeFromBuild.Contains(path))
            {
                return;
            }
            this.assetExcludeFromBuildData.ExcludeFromBuild.Add(path);
            AssetDatabase.SaveAssets();
        }
        void RemoveFromDataConfig(string path)
        {
            if (this.assetExcludeFromBuildData == null)
            {
                return;
            }
            this.assetExcludeFromBuildData.ExcludeFromBuild.Remove(path);
            cacheData.Remove(path);
            AssetDatabase.SaveAssets();
        }

        private void PingPath(string path)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (obj != null)
            {
                EditorGUIUtility.PingObject(obj);
            }
        }

        private void OnGUI()
        {
            this.titleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 20
            };
            GUILayout.Label("Exclude From Build", titleStyle);
            GUILayout.Space(5);
            reorderableList.DoLayoutList();
        }

        private void Validate()
        {
            if (this.assetExcludeFromBuildData == null)
            {
                return;
            }
            foreach (var path in this.assetExcludeFromBuildData.ExcludeFromBuild)
            {
                if (!IsValidPath(path))
                {
                    continue;
                }
                AssetCustomizerMeta.SetFolderMetadata(path, AssetCustomizerMeta.CustomIsExcludedFromBuild, "1");
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public static void ClearCache()
        {
            foreach (var path in cacheData)
            {
                AssetCustomizerMeta.SetFolderMetadata(path, AssetCustomizerMeta.CustomIsExcludedFromBuild, "0");
            }
            cacheData.Clear();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
