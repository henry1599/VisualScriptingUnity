using System.Collections;
using System.Collections.Generic;
using BlueGraph;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowGraph", menuName = "hGraph/FlowGraph")]
public class FlowGraph : Graph
{
    public override string Title => "Flow Graph";
}
