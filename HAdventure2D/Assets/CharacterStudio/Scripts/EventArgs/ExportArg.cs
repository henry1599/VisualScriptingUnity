using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public enum eExportType
    {
        SpriteLibrary,
        SpriteSheet,
        SeparatedSprites
    }
    public class ExportArg : EventArgs
    {
        public eExportType ExportType { get; private set; }
        public string FolderPath { get; set; }
        public ExportArg(eExportType exportType, string folderPath)
        {
            ExportType = exportType;
            FolderPath = folderPath;
        }
    }
    public class SpritesheetExportArg : ExportArg
    {
        public bool AutoSlice {get; private set;}
        public SpritesheetExportArg(string folderPath, bool autoSlice) : base(eExportType.SpriteSheet, folderPath)
        {
            AutoSlice = autoSlice;
        }
    }
    public class SpriteLibraryExportArg : ExportArg
    {
        public SpriteLibraryExportArg(string folderPath) : base(eExportType.SpriteLibrary, folderPath)
        {
        }
    }
    public class SeparatedSpritesExportArg : ExportArg
    {
        public SeparatedSpritesExportArg(string folderPath) : base(eExportType.SeparatedSprites, folderPath)
        {
        }
    }
}
