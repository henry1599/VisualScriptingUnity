using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
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
}
