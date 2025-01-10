using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class FrameIndexUpdateArg : EventArgs
    {
        public int FrameIndex { get; set; }
        public FrameIndexUpdateArg(int frameIndex)
        {
            FrameIndex = frameIndex;
        }
    }
}
