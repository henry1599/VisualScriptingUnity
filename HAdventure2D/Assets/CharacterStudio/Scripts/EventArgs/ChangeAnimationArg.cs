using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class ChangeAnimationArg : EventArgs
    {
        public eCharacterAnimation Animation { get; private set; }
        public ChangeAnimationArg(eCharacterAnimation animation)
        {
            Animation = animation;
        }
    }
}
