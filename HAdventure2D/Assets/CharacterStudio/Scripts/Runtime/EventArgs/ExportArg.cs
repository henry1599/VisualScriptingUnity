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
        public string Name { get; set; }
        public ExportArg(eExportType exportType, string folderPath, string name)
        {
            ExportType = exportType;
            FolderPath = folderPath;
            Name = name;
        }
    }
    public class SpritesheetExportArg : ExportArg
    {
        public bool AutoSlice {get; private set;}
        public SpritesheetExportArg(string folderPath, bool autoSlice, string name) : base(eExportType.SpriteSheet, folderPath, name)
        {
            AutoSlice = autoSlice;
        }
    }
    public class SpriteLibraryExportArg : ExportArg
    {
        public SpriteLibraryExportArg(string folderPath, string name) : base(eExportType.SpriteLibrary, folderPath, name)
        {
        }
    }
    public class SeparatedSpritesExportArg : ExportArg
    {
        public SeparatedSpritesExportArg(string folderPath, string name) : base(eExportType.SeparatedSprites, folderPath, name)
        {
        }
    }
}
