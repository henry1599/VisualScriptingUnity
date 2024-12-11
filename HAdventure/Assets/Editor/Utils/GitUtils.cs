using UnityEngine;

namespace Baram.Utils.EditorTools
{
    public static class GitUtils
    {
        public static string WorkingDir;
        public static string Branch;
        public static void GitShowCurrentWorkingBranch()
        {
            Cmd
                .Append("for /f \"delims=\" %a in ('cmd /c cd') do @for /f \"delims=\" %b in ('git rev-parse --abbrev-ref HEAD') do @echo %a %b")
                .Run(
                    printRunCommand: false,
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        if (string.IsNullOrEmpty(output))
                        {
                            return;
                        }
                        string[] lines = output.Split(" ");
                        if (lines.Length < 2)
                        {
                            return;
                        }
                        WorkingDir = lines[0];
                        Branch = lines[1];
                    }
                );
        }
        [Tooltip("Fetch from and integrate with another repository or a local branch.")]
        [UnityEditor.MenuItem("Tools/Git/git pull")]
        public static void GitPull()
        {
            Cmd
                .Append("git pull")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git pull completed.");
                    }
                );
        }
        [Tooltip("Download objects and refs from another repository.")]
        [UnityEditor.MenuItem("Tools/Git/git fetch")]
        public static void GitFetch()
        {
            Cmd
                .Append("git fetch")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git fecth completed.");
                    }
                );
        }

        [Tooltip("Stash the changes in a dirty working directory away. ")]
        [UnityEditor.MenuItem("Tools/Git/git stash")]
        public static void GitStash()
        {
            Cmd
                .Append("git stash")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git stash completed.");
                    }
                );
        }
        [Tooltip("Apply the last stashed changes to the working directory.")]
        [UnityEditor.MenuItem("Tools/Git/git stash pop")]
        public static void GitStashPop()
        {
            Cmd
                .Append("git stash pop")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git stash pop completed.");
                    }
                );
        }
        [Tooltip("Apply git stash and then git stash pop to clear fake modified files.")]
        [UnityEditor.MenuItem("Tools/Git/git clear fake modified files")]
        public static void GitClearFakeModifiedFiles()
        {
            Cmd
                .Append("git stash")
                .Append("git stash pop")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git clear fake modified files completed.");
                    }
                );
        }
        [Tooltip("Discard all local changes.")]
        [UnityEditor.MenuItem("Tools/Git/git reset --hard")]
        public static void GitResethard()
        {
            if (!UnityEditor.EditorUtility.DisplayDialog("Confirm Git Reset", "Are you sure you want to reset the repository to the last commit? This will discard all local changes.", "Yes", "No"))
            {
                return;
            }
            Cmd
                .Append("git reset --hard")
                .Run(
                    user: eRunUser.Default,
                    onCompleted: output =>
                    {
                        Debug.Log("Git reset --hard completed.");
                    }
                );
        }
    }
}
