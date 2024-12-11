using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

namespace Baram.Game.Editor
{
    public class ExcludeFoldersFromBuild : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public static readonly string DATA_PATH = "Assets/Editor/AssetCustomization/AssetExcludeFromBuildData.asset";
        public int callbackOrder => 0;

        private static string[] foldersToExclude;
        private static string[] tempPaths;

        string[] GatherExcludedAssets()
        {
            var datas = AssetDatabase.FindAssets($"t:{typeof(AssetExcludeFromBuildData)}");
            AssetExcludeFromBuildData data = null;
            if (datas.Length == 0)
            {
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AssetExcludeFromBuildData>(), DATA_PATH);
                data = AssetDatabase.LoadAssetAtPath<AssetExcludeFromBuildData>(DATA_PATH);
            }
            else
            {
                data = AssetDatabase.LoadAssetAtPath<AssetExcludeFromBuildData>(AssetDatabase.GUIDToAssetPath(datas[0]));
            }
            return data.ExcludeFromBuild.ToArray();
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            foldersToExclude = GatherExcludedAssets();
            tempPaths = new string[foldersToExclude.Length];
            //try
            //{
                for (int i = 0; i < foldersToExclude.Length; i++)
                {
                    string sourcePath = foldersToExclude[i];
                    string tempPath = sourcePath + "~";
                    if (Directory.Exists(sourcePath))
                    {
                        if (Directory.Exists(tempPath))
                        {
                            Directory.Delete(tempPath, true);
                        }
                        Directory.Move(sourcePath, tempPath);
                        tempPaths[i] = tempPath;
                    }
                }
            //}
            //finally
            //{
            //    OnPostprocessBuild(report);
            //}
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            for (int i = 0; i < tempPaths.Length; i++)
            {
                if (!string.IsNullOrEmpty(tempPaths[i]) && Directory.Exists(tempPaths[i]))
                {
                    string originalPath = tempPaths[i].Replace("~", "");

                    if (Directory.Exists(originalPath))
                    {
                        Directory.Delete(originalPath, true);
                    }
                    Directory.Move(tempPaths[i], originalPath);

                    string tempMetaPath = tempPaths[i] + ".meta";
                    if (File.Exists(tempMetaPath))
                    {
                        File.Delete(tempMetaPath);
                    }
                }
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Tools/Fix build failure")]
        public static void FixExcludedFolders()
        {
            if (tempPaths == null)
            {
                return;
            }
            for (int i = 0; i < tempPaths.Length; i++)
            {
                if (!string.IsNullOrEmpty(tempPaths[i]) && Directory.Exists(tempPaths[i]))
                {
                    string originalPath = tempPaths[i].Replace("~", "");
                    if (Directory.Exists(originalPath))
                    {
                        Directory.Delete(originalPath, true);
                    }
                    Directory.Move(tempPaths[i], originalPath);
                    string tempMetaPath = tempPaths[i] + ".meta";
                    if (File.Exists(tempMetaPath))
                    {
                        File.Delete(tempMetaPath);
                    }
                }
            }
            AssetDatabase.Refresh();
        }
    }
}
