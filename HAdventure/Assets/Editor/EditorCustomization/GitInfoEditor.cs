using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace Baram.Utils.EditorTools
{
    [InitializeOnLoad]
    public class GitInfoEditor
    {
        static GUIStyle workingDirStyle;
        static string[] commonGitBashPath = new string[]
        {
            @"C:\Program Files\Git\git-bash.exe",
            @"C:\Program Files (x86)\Git\git-bash.exe",
            @"C:\Git\bin\git-bash.exe"
        };
        static GitInfoEditor()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnLeftToolbarGUI);
        }
        static void OnLeftToolbarGUI()
        {
            workingDirStyle ??= new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };
            // GUILayout.Label($"<b>Directory: <color=cyan>{GitUtils.WorkingDir}</color>, Branch: <color=yellow>{GitUtils.Branch}</color></b>", workingDirStyle, GUILayout.ExpandHeight(true)); 
            if (GUILayout.Button($"<b>Directory: <color=cyan>{GitUtils.WorkingDir}</color>, Branch: <color=yellow>{GitUtils.Branch}</color></b>", workingDirStyle, GUILayout.ExpandHeight(true)))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Update git info"), false, () => GitUtils.GitShowCurrentWorkingBranch());
                menu.AddItem(new GUIContent("Cmd here/Default"), false, () => OpenCmd(eRunUser.Default));
                menu.AddItem(new GUIContent("Cmd here/Admin"), false, () => OpenCmd(eRunUser.Admin));
                menu.AddItem(new GUIContent("Git bash here"), false, OpenGitBash);
                menu.AddItem(new GUIContent("Copy Directory"), false, () => EditorGUIUtility.systemCopyBuffer = GitUtils.WorkingDir);
                menu.AddItem(new GUIContent("Copy Branch"), false, () => EditorGUIUtility.systemCopyBuffer = GitUtils.Branch);
                menu.ShowAsContext();
            } 
            GUILayout.FlexibleSpace();
        }
        static void OpenCmd(eRunUser runner = eRunUser.Default)
        {
            Cmd
                .Append("start cmd /K \"cd /d " + GitUtils.WorkingDir + "\"")
                .Run(
                    user: runner
                );
        }
        static void OpenGitBash()
        {
            string gitBashPath = FindGitBashPath();
            if (string.IsNullOrEmpty(gitBashPath))
            {
                Debug.LogError("git-bash.exe not found.");
                return;
            }

            Cmd
                .Append($"start \"\" \"{gitBashPath}\" --cd=\"{GitUtils.WorkingDir}\"")
                .Run(
                    user: eRunUser.Default,
                    onError: output => Debug.LogError(output)
                );
        }

        static string FindGitBashPath()
        {
            string pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(pathEnv))
            {
                string[] paths = pathEnv.Split(';');
                foreach (string path in paths)
                {
                    string potentialPath = Path.Combine(path, "git-bash.exe");
                    if (File.Exists(potentialPath))
                    {
                        return potentialPath;
                    }
                }
            }

            foreach (string commonPath in commonGitBashPath)
            {
                if (File.Exists(commonPath))
                {
                    return commonPath;
                }
            }

            return null;
        }
    }
}