using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphEvents
{
    // * Node type (Class, Variable, Property, Function, Custom), Node name, Node type
    public static Action<eNodeType, string, Type> ON_TOOLBOX_ITEM_CLICKED;
}
