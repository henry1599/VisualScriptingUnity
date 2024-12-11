using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class FolderPaginationWindow : EditorWindow
{
    private string selectedFolderPath = "Assets"; // Default folder
    private string[] folderContents;
    private int currentPage = 0;
    private const int itemsPerPage = 10;

    [MenuItem("Window/Folder Pagination")]
    public static void ShowWindow()
    {
        GetWindow<FolderPaginationWindow>("Folder Pagination");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Folder", EditorStyles.boldLabel);

        // Select folder button
        if (GUILayout.Button("Select Folder"))
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(path))
            {
                selectedFolderPath = "Assets" + path.Replace(Application.dataPath, "").Replace('\\', '/');
                LoadFolderContents();
                currentPage = 0; // Reset to the first page
            }
        }

        GUILayout.Label($"Selected Folder: {selectedFolderPath}");

        if (folderContents != null && folderContents.Length > 0)
        {
            DisplayCurrentPage();

            // Pagination controls
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Previous Page") && currentPage > 0)
                currentPage--;

            GUILayout.Label($"Page {currentPage + 1} of {Mathf.CeilToInt((float)folderContents.Length / itemsPerPage)}");

            if (GUILayout.Button("Next Page") && (currentPage + 1) * itemsPerPage < folderContents.Length)
                currentPage++;
            GUILayout.EndHorizontal();
        }
    }

    private void LoadFolderContents()
    {
        folderContents = Directory.GetFiles(selectedFolderPath)
                                  .Where(path => !path.EndsWith(".meta")) // Exclude .meta files
                                  .ToArray();
    }

    private void DisplayCurrentPage()
    {
        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, folderContents.Length);

        for (int i = startIndex; i < endIndex; i++)
        {
            GUILayout.Label(Path.GetFileName(folderContents[i]));
        }
    }
}
