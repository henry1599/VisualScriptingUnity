using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace Baram.Utils.EditorTools
{
    [ExecuteInEditMode]
    public static partial class Common
    {
        public static void SaveCurrentScenesToPrefs()
        {
            int sceneCount = EditorSceneManager.sceneCount;
            EditorPrefs.SetInt("SceneCount", sceneCount);
            for (int i = 0; i < sceneCount; i++)
            {
                Scene scene = EditorSceneManager.GetSceneAt(i);
                OpenSceneMode sceneMode;
                if (scene.isLoaded)
                    sceneMode = OpenSceneMode.Additive;
                else
                    sceneMode = OpenSceneMode.AdditiveWithoutLoading;
                if (EditorSceneManager.GetActiveScene() == scene)
                    sceneMode = OpenSceneMode.Single;
                EditorPrefs.SetString($"Scene{i}Path", scene.path);
                EditorPrefs.SetInt($"Scene{i}Mode", (int)sceneMode);
            }
        }

        public static void RemovePrefs()
        {
            int sceneCount = EditorPrefs.GetInt("SceneCount");
            for (int i = 0; i < sceneCount; i++)
            {
                EditorPrefs.DeleteKey($"Scene{i}Path");
                EditorPrefs.DeleteKey($"Scene{i}Mode");
            }
        }
        public static string ToReadableFormat(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // * Result string will never exceed x2 the length of the input string
            var newText = new StringBuilder(input.Length * 2);
            newText.Append(char.ToUpper(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && !char.IsUpper(input[i - 1]))
                    newText.Append(' ');

                newText.Append(input[i]);
            }

            return newText.ToString();
        }
        public static List<SerializedProperty> GetPropertiesFromField(SerializedObject serializedObject, FieldInfo field)
        {
            List<SerializedProperty> props = new List<SerializedProperty>();
            for (int i = 0; i < serializedObject.targetObjects.Length; i++)
            {
                SerializedObject obj = new SerializedObject(serializedObject.targetObjects[i]);
                SerializedProperty prop = obj.FindProperty(field.Name);
                props.Add(prop);
            }
            return props;
        }
        public static string ReadFromFile(string path)
        {
            string result = "";
            if (System.IO.File.Exists(path))
            {
                result = System.IO.File.ReadAllText(path);
            }
            return result;
        }
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                hashSet.Add(item);
            }
        }
        public static Texture2D MakeTex2D(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
        public static long GetFileSize(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.FileInfo info = new System.IO.FileInfo(path);
                return info.Length;
            }
            return 0;
        }

        // * Recursively get the size of a folder
        public static long GetFolderSize(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                long size = 0;
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if (file.EndsWith(".meta"))
                        continue;
                    size += GetFileSize(file);
                }
                string[] folders = System.IO.Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    size += GetFolderSize(folder);
                }
                return size;
            }
            return 0;
        }
        public static long GetFolderImageSize(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                long size = 0;
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if (file.EndsWith(".png") || file.EndsWith(".jpg"))
                    {
                        size += GetFileSize(file);
                    }
                }
                string[] folders = System.IO.Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    size += GetFolderImageSize(folder);
                }
                return size;
            }
            return 0;
        }
        public static float ByteToKB(this float bytes)
        {
            return bytes / 1024f;
        }
        public static float ByteToMB(this float bytes)
        {
            return bytes / 1024f / 1024f;
        }
        public static float ByteToGB(this float bytes)
        {
            return bytes / 1024f / 1024f / 1024f;
        }
    }
}