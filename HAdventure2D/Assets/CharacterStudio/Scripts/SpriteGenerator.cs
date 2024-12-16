using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.IO;

public class SpriteGenerator : EditorWindow
{
    private Dictionary<Color32, Color32> mappedColors = new Dictionary<Color32, Color32>();
    static SpriteGenerator window;
    private List<Texture2D> maps = new List<Texture2D>();
    private Texture2D baseTexture;
    private Texture2D baseMap;
    private ReorderableList reorderableList;
    private string savePath = "Assets/CharacterStudio/Sprites/";

    [MenuItem("Character Studio/Sprite Generator")]
    public static void ShowWindow()
    {
        window = GetWindow<SpriteGenerator>("Sprite Generator");
        window.minSize = new Vector2(400, 200);
    }

    private void OnEnable()
    {
        reorderableList = new ReorderableList(maps, typeof(Texture2D), true, true, true, true);
        reorderableList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Maps");
        };
        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            maps[index] = (Texture2D)EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), maps[index], typeof(Texture2D), false);
        };
        reorderableList.onAddCallback = (ReorderableList list) =>
        {
            maps.Add(null);
        };
        reorderableList.onRemoveCallback = (ReorderableList list) =>
        {
            if (maps.Count > 0)
            {
                maps.RemoveAt(list.index);
            }
        };
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        baseTexture = (Texture2D)EditorGUILayout.ObjectField("Base Texture", baseTexture, typeof(Texture2D), false);
        baseMap = (Texture2D)EditorGUILayout.ObjectField("Base Map", baseMap, typeof(Texture2D), false);

        reorderableList.DoLayoutList();

        if (GUILayout.Button("Generate Sprites"))
        {
            GenerateAllSprites();
        }
    }

    public void LoadMappedColors()
    {
        mappedColors.Clear();
        for (int x = 0; x < baseMap.width; x++)
        {
            for (int y = 0; y < baseMap.height; y++)
            {
                Color32 baseMapColor = baseMap.GetPixel(x, y);
                if (baseMapColor.a == 0)
                {
                    continue;
                }
                Color32 baseTextureColor = baseTexture.GetPixel(x, y);
                mappedColors.TryAdd(baseMapColor, baseTextureColor);
            }
        }
    }

    public void GenerateSprite(Texture2D map)
    {
        Texture2D newTexture = new Texture2D(baseMap.width, baseMap.height, TextureFormat.RGBA32, false);
        for (int x = 0; x < baseMap.width; x++)
        {
            for (int y = 0; y < baseMap.height; y++)
            {
                Color32 baseMapColor = map.GetPixel(x, y);
                if (baseMapColor.a == 0)
                {
                    newTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));
                    continue;
                }
                if (mappedColors.TryGetValue(baseMapColor, out Color32 baseTextureColor))
                {
                    baseTextureColor.a = 255;
                    newTexture.SetPixel(x, y, baseTextureColor);
                }
                else
                {
                    newTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));
                }
            }
        }
        newTexture.Apply();
        byte[] bytes = newTexture.EncodeToPNG();
        string filePath = savePath + map.name + ".png";
        File.WriteAllBytes(filePath, bytes);

        // Load the saved texture again
        AssetDatabase.ImportAsset(filePath);
        TextureImporter importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = 16; // Adjust this value if necessary
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.alphaIsTransparency = true;
            importer.isReadable = true;

            TextureImporterPlatformSettings texset = importer.GetDefaultPlatformTextureSettings();
            texset.format = TextureImporterFormat.RGBA32;
            texset.maxTextureSize = 2048; // Adjust this value if necessary
            importer.SetPlatformTextureSettings(texset);

            importer.SaveAndReimport();
        }
    }

    public void GenerateAllSprites()
    {
        LoadMappedColors();
        foreach (Texture2D map in maps)
        {
            if (map != null)
            {
                GenerateSprite(map);
            }
        }
    }
}