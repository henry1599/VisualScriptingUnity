using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class ExportResult
    {
        public string OutputPath {get; set;}
    }
    public class SeparatedSpriteResult : ExportResult
    {
        public Dictionary<string, Texture2D> Sprites {get; set;}
    }
    public class SpriteSheetResult : ExportResult
    {
        public Texture2D SpriteSheet {get; set;}
        public List<(eCharacterAnimation anim, int frameCount)> FrameCount {get; set;}
    }
    public class SpriteLibrarayResult : ExportResult
    {
        
    }
}
