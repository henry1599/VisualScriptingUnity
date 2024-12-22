using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.IO;

public class SpriteGenerator : MonoBehaviour
{
    private Dictionary<Color32, Color32> mappedColors = new Dictionary<Color32, Color32>();
    static SpriteGenerator window;
    private List<Texture2D> maps = new List<Texture2D>();
    

    
}