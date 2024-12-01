using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Icon("d_Skybox Icon")]
public class hBehaviour : MonoBehaviour
{
    [Searchable]
    public List<int> testList = new List<int>();
    [Button]
    public void TestButton()
    {

    }
}
