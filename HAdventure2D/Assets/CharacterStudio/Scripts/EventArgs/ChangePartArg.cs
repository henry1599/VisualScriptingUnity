using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    // Broadcasted when user taps on a part in the character studio
    public class ChangePartArg : EventArgs
    {
        public eCharacterPart Part { get; private set; }
        public string Id { get; private set; }
        public ChangePartArg(eCharacterPart part, string id)
        {
            Part = part;
            Id = id;
        }
    }

    // Broadcasted when a part is changed 
    public class PartChangedArg : EventArgs
    {
        public eCharacterPart Part { get; private set; }
        public string Id { get; private set; }
        public PartChangedArg( eCharacterPart part, string id )
        {
            Part = part;
            Id = id;
        }
    }
}
