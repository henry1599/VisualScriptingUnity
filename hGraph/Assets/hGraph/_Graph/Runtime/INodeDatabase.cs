using System.Collections;
using System.Collections.Generic;
using BlueGraph;
using UnityEngine;

public interface INodeDatabase
{
    string Title { get; }
    T GetNode<T>() where T : Node;
    IEnumerable<T> GetNodes<T>() where T : Node;
}
