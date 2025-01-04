using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class CSState
    {
        public Color[] PixelColors;
        public CSState(CSState state)
        {
            PixelColors = state.PixelColors;
        }
        public CSState(Color[] pixelColor)
        {
            PixelColors = pixelColor;
        }
        public override bool Equals(object obj)
        {
            CSState other = obj as CSState;
            if (other == null)
                return false;
            return other.PixelColors.Equals(PixelColors);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class RegisterStateArg: EventArgs
    {
        public CSState State;
        public RegisterStateArg(CSState state)
        {
            State = state;
        }
    }
}
