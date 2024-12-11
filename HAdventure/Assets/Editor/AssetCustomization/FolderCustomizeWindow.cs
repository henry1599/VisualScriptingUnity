using UnityEditor;
using UnityEngine;
public static class StringExtensions
{
    public static string ReplaceAny(this string str, string[] oldValues, string newValue)
    {
        foreach (var oldValue in oldValues)
        {
            str = str.Replace(oldValue, newValue);
        }
        return str;
    }
}
public class FolderCustomizeWindow : EditorWindow
{
    private static string folderPath;
    private Color selectedColor;
    private Color selectedTextColor;
    private bool useGradient = true;
    private string tooltip;
    private bool advanceFoldout;
    private int chosenAlignment = 0;
    private int chosenStyle = 0;
    private bool needToShowWarningOnOpen = false;
    private string warningMessage = AssetCustomizerMeta.DefaultWarningMessage;
    private string[] restrictStringInTooltip = new string[] { "-", ";", ":" };

    // Open the window and pass the selected folder path
    public static void Open(string path)
    {
        folderPath = path;
        FolderCustomizeWindow window = GetWindow<FolderCustomizeWindow>("Customize Folder");
        window.selectedColor = AssetCustomizerMeta.LoadFolderColor(path);
        window.selectedTextColor = AssetCustomizerMeta.LoadFolderTextColor(path);
        window.tooltip = AssetCustomizerMeta.LoadFolderTooltip(path);
        if (!int.TryParse(AssetCustomizerMeta.LoadAlignment(path), out window.chosenAlignment))
        {
            window.chosenAlignment = 0;
        }
        if (!int.TryParse(AssetCustomizerMeta.LoadStyle(path), out window.chosenStyle))
        {
            window.chosenStyle = 0;
        }
        if (!bool.TryParse(AssetCustomizerMeta.LoadUseGradient(path), out window.useGradient))
        {
            window.useGradient = true;
        }
        window.minSize = new Vector2(300, 300);
        window.Show();
    }

    void OnGUI()
    {
        // Display the color picker
        this.selectedColor = EditorGUILayout.ColorField("Folder Color:", selectedColor);
        this.selectedTextColor = EditorGUILayout.ColorField("Text Color:", this.selectedTextColor);

        EditorGUILayout.LabelField("Tooltip:");
        this.tooltip = EditorGUILayout.TextArea(this.tooltip, GUILayout.Height(60));
        this.tooltip = this.tooltip.ReplaceAny(restrictStringInTooltip, "");

        this.needToShowWarningOnOpen = EditorGUILayout.Toggle("Show Warning On Open", this.needToShowWarningOnOpen);
        if (this.needToShowWarningOnOpen)
        {
            this.warningMessage = EditorGUILayout.TextField("Warning Message", this.warningMessage);
        }

        this.advanceFoldout = EditorGUILayout.Foldout(this.advanceFoldout, "Advance Options");

        if (this.advanceFoldout)
        {
            this.useGradient = EditorGUILayout.Toggle("Use Gradient", this.useGradient);
            var alignLeftIcon = EditorGUIUtility.IconContent("align_horizontally_left");
            var alignCenterIcon = EditorGUIUtility.IconContent("align_horizontally_center");
            var alignRightIcon = EditorGUIUtility.IconContent("align_horizontally_right");

            if (!this.useGradient)
                this.chosenAlignment = GUILayout.Toolbar(this.chosenAlignment, new GUIContent[] { alignLeftIcon, alignCenterIcon, alignRightIcon }, GUILayout.Height(30));
            this.chosenStyle = EditorGUILayout.Popup("Style", this.chosenStyle, new string[] { "Normal", "Bold", "Italic" });
        }

        // Apply and save the color
        if (GUILayout.Button("Apply"))
        {
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomColorKey, $"{ColorUtility.ToHtmlStringRGBA(this.selectedColor)}-{this.selectedColor.a}");
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomTextColorKey, $"{ColorUtility.ToHtmlStringRGBA(this.selectedTextColor)}-{this.selectedTextColor.a}");
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomTooltipKey, this.tooltip);
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomAlignmentKey, this.chosenAlignment.ToString());
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomStyleKey, this.chosenStyle.ToString());
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomUseGradientKey, this.useGradient.ToString());
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomNeedToShowWarningOnOpenKey, this.needToShowWarningOnOpen.ToString());
            AssetCustomizerMeta.SetFolderMetadata(folderPath, AssetCustomizerMeta.CustomWarningMessageKey, this.warningMessage);
            AssetDatabase.Refresh();  // Refresh the Project window to apply changes
            Close();
        }
    }
}
