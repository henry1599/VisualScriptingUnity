using System;

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
