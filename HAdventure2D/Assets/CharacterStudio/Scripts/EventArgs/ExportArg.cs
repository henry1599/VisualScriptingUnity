using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public enum eExportType
    {
        All,
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
}
