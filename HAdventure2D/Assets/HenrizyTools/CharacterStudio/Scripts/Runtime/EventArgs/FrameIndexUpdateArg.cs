using System;

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
