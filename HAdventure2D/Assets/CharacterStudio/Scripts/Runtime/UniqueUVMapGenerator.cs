using UnityEngine;

public class UniqueUVMapGenerator : MonoBehaviour
{
    public int textureSize = 48; // Size of the texture (48x48)

    void Start()
    {
        Texture2D texture = GenerateUVMap(textureSize);
        SaveTextureAsPNG(texture, Application.dataPath + "/UVMap_48x48.png");
    }

    Texture2D GenerateUVMap(int size)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        int step = 255 / size; // Difference in color per cell, ~5 for 48x48
        
        // First row: Red channel
        for (int x = 0; x < size; x++)
        {
            int redValue = x * step;
            texture.SetPixel(x, 0, new Color(redValue / 255f, 0f, 0f, 1f));
        }
        
        // First column: Green channel
        for (int y = 0; y < size; y++)
        {
            int greenValue = y * step;
            texture.SetPixel(0, y, new Color(0f, greenValue / 255f, 0f, 1f));
        }
        
        // Remaining pixels: Combine row's R and column's G
        for (int y = 1; y < size; y++) // Start from y = 1 (skip first column)
        {
            int greenValue = y * step;

            for (int x = 1; x < size; x++) // Start from x = 1 (skip first row)
            {
                int redValue = x * step;
                texture.SetPixel(x, y, new Color(redValue / 255f, greenValue / 255f, 0f, 1f));
            }
        }

        texture.Apply();
        return texture;
    }

    void SaveTextureAsPNG(Texture2D texture, string path)
    {
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("UV Map saved to: " + path);
    }
}
