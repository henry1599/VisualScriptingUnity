using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class DefaultPartData
    {
        public eCharacterPart Part;
        public string DefaultPart;
    }
    [CreateAssetMenu( fileName = "DefaultPartDataList", menuName = "CharacterStudio/Configs/Default Part Data List" )]
    public class DefaultPartDataList : CSJson
    {
        public List<DefaultPartData> DefaultParts;
    }
}
