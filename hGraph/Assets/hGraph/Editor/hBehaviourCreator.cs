using System.IO;
using UnityEditor;
using UnityEngine;

public class hBehaviourCreator : Editor
{
    [MenuItem("Assets/Create/hBehaviour", false, 80)] // Creates a menu item under 'Assets > Create'
    private static void CreateHBehaviourTemplate()
    {
        var icon = EditorGUIUtility.IconContent("d_Skybox Icon");
        string templatePath = Application.dataPath + "/hGraph/Template.txt";
        string template = File.ReadAllText(templatePath);

        // Default filename
        string defaultFileName = "NewHBehaviour.cs";

        // Prompt for filename and save path
        string path = EditorUtility.SaveFilePanelInProject(
            "Create hBehaviour Script",
            defaultFileName,
            "cs",
            "Enter a name for the new hBehaviour script."
        );

        // If user cancels the operation
        if (string.IsNullOrEmpty(path))
            return;

        // Extract script name from the provided path
        string scriptName = Path.GetFileNameWithoutExtension(path);

        // Replace the placeholder with the actual script name
        string scriptContent = template.Replace("#SCRIPTNAME#", scriptName);

        // Write the file to the specified path
        File.WriteAllText(path, scriptContent);
        AssetDatabase.Refresh();

        // Automatically focus on and rename the created file
        Object newScript = AssetDatabase.LoadAssetAtPath<Object>(path);
        ProjectWindowUtil.ShowCreatedAsset(newScript);
        if (icon != null)
        {
            // Assign icon to the created file in unity editor
        }
    }
}
