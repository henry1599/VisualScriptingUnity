using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Searchable]
    public List<int> testList = new List<int>();
        // [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)] public HDictionary<string, ParsedMethodList> ClassMethods;
    [Button]
    public void TestButton()
    {

    }
}
