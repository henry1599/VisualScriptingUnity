using Baram.Game.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UserMetaData
{
    public Dictionary<string, string> MetaData;
    public UserMetaData()
    {
        MetaData = new Dictionary<string, string>();
    }
    public void AddMetaData(string key, string value)
    {
        if (MetaData.ContainsKey(key))
        {
            MetaData[key] = value;
        }
        else
        {
            MetaData.Add(key, value);
        }
    }
    public string ToUserData()
    {
        List<string> userdata = new List<string>();
        foreach (var item in MetaData)
        {
            userdata.Add($"{item.Key}:{item.Value}");
        }
        return string.Join(";", userdata);
    }
    public static UserMetaData FromUserData(string userData)
    {
        UserMetaData metaData = new UserMetaData();
        string[] data = userData.Split(';');
        foreach (var item in data)
        {
            string[] keyValue = item.Split(':');
            if (keyValue.Length == 2)
                metaData.AddMetaData(keyValue[0], keyValue[1]);
        }
        return metaData;
    }
    public string GetValue(string key)
    {
        if (MetaData.ContainsKey(key))
        {
            return MetaData[key];
        }
        return null;
    }
}
public class AssetCustomizeData
{
    public bool IsExcludedFromBuild;
    public Color BackgroundColor;
    public Color TextColor;
    public string Tooltip;
    public int AlignmentIndex; // 0: Left, 1: Center, 2: Right
    public int StyleIndex; // 0: Normal, 1: Bold, 2: Italic
    public bool UseGradient;
    public string Path;
    public string Name;
    public bool NeedShowWarningOnOpen;
    public string WarningMessage;
    public Rect Rect;
    public AssetCustomizeData()
    {
        BackgroundColor = AssetCustomizerMeta.DefaultEditorColor;
        TextColor = AssetCustomizerMeta.DefaultTextColor;
        Tooltip = "";
        AlignmentIndex = 0;
        StyleIndex = 1;
        UseGradient = true;
        NeedShowWarningOnOpen = false;
        WarningMessage = AssetCustomizerMeta.DefaultWarningMessage;
        IsExcludedFromBuild = false;
    }
    public void Validate()
    {
        if (UseGradient)
        {
            AlignmentIndex = 0;
        }
    }
    public static AssetCustomizeData Create(string path, Rect selectedRect)
    {
        return new AssetCustomizeData()
        {
            Path = path,
            Name = System.IO.Path.GetFileName(path),
            Rect = selectedRect,
            BackgroundColor = AssetCustomizerMeta.LoadFolderColor(path),
            TextColor = AssetCustomizerMeta.LoadFolderTextColor(path),
            Tooltip = AssetCustomizerMeta.LoadFolderTooltip(path),
            AlignmentIndex = int.TryParse(AssetCustomizerMeta.LoadAlignment(path), out int alignment) ? alignment : 0,
            StyleIndex = int.TryParse(AssetCustomizerMeta.LoadStyle(path), out int style) ? style : 0,
            UseGradient = bool.TryParse(AssetCustomizerMeta.LoadUseGradient(path), out bool useGradient) ? useGradient : true,
            NeedShowWarningOnOpen = bool.TryParse(AssetCustomizerMeta.LoadNeedShowWarningOnOpen(path), out bool needShowWarning) ? needShowWarning : false,
            WarningMessage = AssetCustomizerMeta.LoadWarningMessage(path),
            IsExcludedFromBuild = AssetCustomizerMeta.LoadIsExcludedFromBuild(path)
        };
    }
    public void ToMeta()
    {
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomColorKey, $"{ColorUtility.ToHtmlStringRGBA(BackgroundColor)}-{BackgroundColor.a}");
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomTextColorKey, $"{ColorUtility.ToHtmlStringRGBA(TextColor)}-{TextColor.a}");
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomTooltipKey, Tooltip);
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomAlignmentKey, AlignmentIndex.ToString());
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomStyleKey, StyleIndex.ToString());
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomUseGradientKey, UseGradient ? "1" : "0");
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomNeedToShowWarningOnOpenKey, NeedShowWarningOnOpen ? "1" : "0");
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomWarningMessageKey, WarningMessage);
        AssetCustomizerMeta.SetFolderMetadata(Path, AssetCustomizerMeta.CustomIsExcludedFromBuild, IsExcludedFromBuild ? "1" : "0");
    }
}
public class AssetCustomizerMeta : AssetPostprocessor
{
    public static readonly string CustomIsExcludedFromBuild = "IsExcludedFromBuild";
    public static readonly string CustomColorKey = "FolderColor";
    public static readonly string CustomTextColorKey = "TextColor";
    public static readonly string CustomTooltipKey = "Tooltip";
    public static readonly string CustomAlignmentKey = "Alignment";
    public static readonly string CustomStyleKey = "Style";
    public static readonly string CustomUseGradientKey = "UseGradient";
    public static readonly string CustomNeedToShowWarningOnOpenKey = "NeedToShowWarningOnOpen";
    public static readonly string CustomWarningMessageKey = "WarningMessage";
    public static readonly string DefaultWarningMessage = "Are you sure you want to open this folder?";
    public static readonly Color DefaultEditorColor = new Color(56f / 255f, 56f / 255f, 56f / 255f);
    public static readonly Color DefaultEditorColorNoAlpha = new Color(56f / 255f, 56f / 255f, 56f / 255f, 0f);
    public static readonly Color DefaultEditorSelectedColor = new Color(44f / 255f, 93f / 255f, 135f / 255f);
    public static readonly Color DefaultTextColor = new Color(1f, 1f, 1f, 1f);


    [MenuItem("Assets/Customize/Open Window", false, 1001)]
    private static void CustomizeFolder()
    {
        string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);

        if (AssetDatabase.IsValidFolder(path))
        {
            FolderCustomizeWindow.Open(path);
        }
    }
    [MenuItem("Assets/Exclude From Build", false, 1003)]
    private static void ExcludeFolderFromBuild()
    {
        string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
        AssetExcludeFromBuildData data = AssetDatabase.LoadAssetAtPath<AssetExcludeFromBuildData>(ExcludeFoldersFromBuild.DATA_PATH);
        if (data == null)
        {
            data = ScriptableObject.CreateInstance<AssetExcludeFromBuildData>();
            data.ExcludeFromBuild.Add(path);
            AssetDatabase.CreateAsset(data, ExcludeFoldersFromBuild.DATA_PATH);
        }
        AssetDatabase.Refresh();
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null)
        {
            if (LoadIsExcludedFromBuild(path))
            {
                SetFolderMetadata(path, CustomIsExcludedFromBuild, "0");
                string tooltip = LoadFolderTooltip(path);
                if (!string.IsNullOrEmpty(tooltip) && tooltip.StartsWith("(Excluded) "))
                {
                    tooltip = tooltip.Replace("(Excluded) ", "");
                }
                data.ExcludeFromBuild.Remove(path);
            }
            else
            {
                SetFolderMetadata(path, CustomIsExcludedFromBuild, "1");
                string tooltip = LoadFolderTooltip(path);
                if (!string.IsNullOrEmpty(tooltip))
                {
                    tooltip = "(Excluded) " + tooltip;
                }
                if (!data.ExcludeFromBuild.Contains(path))
                {
                    data.ExcludeFromBuild.Add(path);
                }
            }
        }
    }
    [MenuItem("Assets/Customize/Reset", false, 1002)]
    private static void ResetFolder()
    {
        string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);

        if (AssetDatabase.IsValidFolder(path))
        {
            ResetFolderMetaData(path, CustomColorKey); 
            ResetFolderMetaData(path, CustomTextColorKey);
            ResetFolderMetaData(path, CustomTooltipKey);
            ResetFolderMetaData(path, CustomAlignmentKey);
            ResetFolderMetaData(path, CustomStyleKey);
            ResetFolderMetaData(path, CustomUseGradientKey);
            ResetFolderMetaData(path, CustomNeedToShowWarningOnOpenKey);
            ResetFolderMetaData(path, CustomWarningMessageKey);
            ResetFolderMetaData(path, CustomIsExcludedFromBuild);

            AssetDatabase.Refresh();
        }
    }


    [MenuItem("Assets/Customize/Open Window", true)]
    private static bool ValidateCustomizeFolder()
    {
        if (Selection.assetGUIDs.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            return AssetDatabase.IsValidFolder(path);
        }
        return false;
    }
    [MenuItem("Assets/Customize/Reset", true)]
    private static bool ValidateResetFolder()
    {
        if (Selection.assetGUIDs.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            return AssetDatabase.IsValidFolder(path);
        }
        return false;
    }

    public static void SetFolderMetadata(string path, string key, string value)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null)
        {
            UserMetaData metaData = UserMetaData.FromUserData(importer.userData);
            metaData.AddMetaData(key, value);
            importer.userData = metaData.ToUserData();
            importer.SaveAndReimport();
        }
    }
    public static void ResetFolderMetaData(string path, string key)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null)
        {
            UserMetaData userMetaData = UserMetaData.FromUserData(importer.userData);
            userMetaData.MetaData.Remove(key);
            importer.userData = userMetaData.ToUserData();
            importer.SaveAndReimport();
        }
    }
    public static bool LoadIsExcludedFromBuild(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomIsExcludedFromBuild);
            return value == "1";
        }
        return false;
    }
    public static Color LoadFolderColor(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomColorKey);
            string[] splitValues = value?.Split('-');
            string hexValue = "000000";
            string alpha = "1";
            if (splitValues != null && splitValues.Length == 2)
            {
                hexValue = splitValues[0];
                alpha = splitValues[1];
            }
            if (!string.IsNullOrEmpty(value))
            {
                if (ColorUtility.TryParseHtmlString("#" + hexValue, out Color color))
                {
                    if (float.TryParse(alpha, out float alphaValue))
                    {
                        color.a = alphaValue;
                    }
                    return color;
                }
            }
        }
        return DefaultEditorColor;
    }
    public static Color LoadFolderTextColor(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomTextColorKey);
            string[] splitValues = value?.Split('-');
            string hexValue = "000000";
            string alpha = "1";
            if (splitValues != null && splitValues.Length == 2)
            {
                hexValue = splitValues[0];
                alpha = splitValues[1];
            }
            if (!string.IsNullOrEmpty(value))
            {
                if (ColorUtility.TryParseHtmlString("#" + hexValue, out Color color))
                {
                    if (float.TryParse(alpha, out float alphaValue))
                    {
                        color.a = alphaValue;
                    }
                    return color;
                }
            }
        }
        return DefaultTextColor;
    }
    public static string LoadFolderTooltip(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomTooltipKey);
            return value;
        }
        return "";
    }
    public static string LoadAlignment(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomAlignmentKey);
            return value;
        }
        return "";
    }
    public static string LoadStyle(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomStyleKey);
            return value;
        }
        return "";
    }
    public static string LoadUseGradient(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomUseGradientKey);
            return value;
        }
        return "1";
    }
    public static string LoadNeedShowWarningOnOpen(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomNeedToShowWarningOnOpenKey);
            return value;
        }
        return "";
    }
    public static string LoadWarningMessage(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        if (importer != null && !string.IsNullOrEmpty(importer.userData))
        {
            string value = UserMetaData.FromUserData(importer.userData).GetValue(CustomWarningMessageKey);
            return value;
        }
        return DefaultWarningMessage;
    }
    public static bool HasCustomized(string path)
    {
        var importer = AssetImporter.GetAtPath(path);
        return importer != null && !string.IsNullOrEmpty(importer.userData);
    }
}
public enum GradientDirection
{
    Horizontal,
    Vertical
}

[InitializeOnLoad]
public class AssetIconOverlay
{

    static AssetIconOverlay()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
    }


    private static void DrawOverrideFolderLayout(string guid, Rect selectionRect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        if (!AssetDatabase.IsValidFolder(path))
            return;
        
        bool isSelected = Selection.assetGUIDs.Contains(guid);
        Color folderColor = isSelected ? AssetCustomizerMeta.DefaultEditorSelectedColor : AssetCustomizerMeta.DefaultEditorColor;
        Rect rect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height);
        EditorGUI.DrawRect(rect, folderColor);
    }
    private static bool IsMainListAsset(Rect rect)
    {
        if (rect.height > 20)
        {
            return false;
        }
        return true;
    }
    private static void DrawCustomizeFolder(string path, string guid, Rect selectionRect)
    {
        DrawOverrideFolderLayout(guid, selectionRect);
        AssetCustomizeData data = AssetCustomizeData.Create(path, selectionRect);
        DrawCustomizeFolder(data);
    }
    private static void DrawCustomizeFolder(AssetCustomizeData data)
    {
        GUIStyle style = new GUIStyle(EditorStyles.label);

        float width = data.UseGradient ? data.Rect.width : style.CalcSize(new GUIContent(data.Name)).x + 20;
        Rect rect = default; 
        if (data.UseGradient)
        {
            int buffer = 30;
            rect = new Rect(data.Rect.x, data.Rect.y, width, data.Rect.height * 1);
            Texture2D gradientTextureForward = CreateGradientTexture(data.BackgroundColor, AssetCustomizerMeta.DefaultEditorColorNoAlpha, (int)data.Rect.width, GradientDirection.Horizontal);
            Texture2D gradientTextureBackward = CreateGradientTexture(AssetCustomizerMeta.DefaultEditorColorNoAlpha, data.BackgroundColor, buffer, GradientDirection.Horizontal);
            if (gradientTextureForward != null)
            {
                GUI.DrawTexture(rect, gradientTextureForward);
            }
            rect.x = data.Rect.x - buffer;
            rect.width = buffer + 5;
            if (gradientTextureBackward != null)
            {
                GUI.DrawTexture(rect, gradientTextureBackward);
            }
        }
        else
        {
            rect = new Rect(data.Rect.x, data.Rect.y, width, data.Rect.height);
            EditorGUI.DrawRect(rect, data.BackgroundColor);
        }

        // Draw the gradient texture


        int alignmentIndex = data.AlignmentIndex;
        int styleIndex = data.StyleIndex;
        

        GUIStyle boldStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = alignmentIndex switch
            {
                0 => TextAnchor.MiddleLeft,
                1 => TextAnchor.MiddleCenter,
                2 => TextAnchor.MiddleRight,
                _ => TextAnchor.MiddleLeft,
            },
            fontStyle = styleIndex switch
            {
                0 => FontStyle.Normal,
                1 => FontStyle.Bold,
                2 => FontStyle.Italic,
                _ => FontStyle.Normal,
            },
            normal = { textColor = data.TextColor },
        };
        var folderIcon = EditorGUIUtility.FindTexture("Folder Icon");
        var excludeIcon = EditorGUIUtility.IconContent("d_PrefabOverlayRemoved Icon");
        Rect iconRect = new Rect(data.Rect.x, data.Rect.y, 16, 16);
        Rect excludedIconRect = new Rect(data.Rect.x, data.Rect.y, 16, 16);
        GUI.DrawTexture(iconRect, folderIcon);
        if (data.IsExcludedFromBuild)
        {
            GUI.DrawTexture(excludedIconRect, excludeIcon.image);
        }

        Rect labelRect = new Rect(data.Rect.x + 18, data.Rect.y, width, data.Rect.height);
        GUIContent content = new GUIContent(data.Name, data.Tooltip);

        EditorGUI.LabelField(labelRect, content, boldStyle);
    }
    private static void DrawOverlayExcludeIcon(string path, string guid, Rect selectionRect)
    {
        var excludeIcon = EditorGUIUtility.IconContent("d_PrefabOverlayRemoved Icon");
        AssetCustomizeData data = AssetCustomizeData.Create(path, selectionRect); 
        Rect excludedIconRect = new Rect(data.Rect.x, data.Rect.y, 16, 16);
        if (data.IsExcludedFromBuild)
        {
            GUI.DrawTexture(excludedIconRect, excludeIcon.image);
        }
    }
    private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);  
        DrawOverlayExcludeIcon(path, guid, selectionRect);
        if (AssetDatabase.IsValidFolder(path))
        {
            bool hasBeenCustomized = AssetCustomizerMeta.HasCustomized(path);
            if (hasBeenCustomized)
            {
                if (!IsMainListAsset(selectionRect))
                {
                    return;
                }
                DrawCustomizeFolder(path, guid, selectionRect);
            }
        }
    }
    // Direction = 0: Vertical, 1: Horizontal
    private static Texture2D CreateGradientTexture(Color startColor, Color endColor, int size, GradientDirection direction)
    {
        try
        {
            int resolution = size * 20;
            int width = direction == GradientDirection.Horizontal ? resolution : 1;
            int height = direction == GradientDirection.Vertical ? resolution : 1;
            width = Mathf.Clamp(Mathf.Abs(width), 1, 200);
            height = Mathf.Clamp(Mathf.Abs(height), 1, 200);
            Texture2D texture = new Texture2D(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float t = direction == GradientDirection.Horizontal ? (float)x / (width - 1) : (float)y / (height - 1);
                    Color color = Color.Lerp(startColor, endColor, t);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();
            return texture;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
