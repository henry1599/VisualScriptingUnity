using Baram.Game.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetExcludeFromBuildData : ScriptableObject
{
    public List<string> ExcludeFromBuild = new List<string>();
}
