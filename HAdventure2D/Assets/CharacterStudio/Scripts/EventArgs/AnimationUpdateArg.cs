using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class AnimationUpdateArg : EventArgs
    {
        public eCharacterAnimation AnimationType { get; set; }
        public AnimationUpdateArg(eCharacterAnimation animationType)
        {
            AnimationType = animationType;
        }
    }
}
