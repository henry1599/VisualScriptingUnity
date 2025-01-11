using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class SortedData
    {
        public eCharacterPart Part;
        public int Order;
    }
    [CreateAssetMenu( fileName = "SortedDataList", menuName = "CharacterStudio/Configs/SortedDataList" )]
    public class SortedDataList : CSJson
    {
        public List<SortedData> SortedData;
        // Example json
        // {
        //     "SortedData": [
        //         {
        //             "Part": 1,
        //             "Order": 1
        //         },
        //  {
    }
}
