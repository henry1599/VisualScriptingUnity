using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using UnityEditor;
using UnityEngine;

public static class Common
{
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    public static List<FieldInfo> GetFieldList(this Type scriptType, List<string> namespaces)
    {
        var fields = new List<FieldInfo>();
        fields.AddRange(scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return fields;
    }
    public static List<PropertyInfo> GetPropertyList(this Type scriptType, List<string> namespaces)
    {
        var properties = new List<PropertyInfo>();
        properties.AddRange(scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return properties;
    }

    public static List<MethodInfo> GetMethodList(this Type scriptType, List<string> namespaces)
    {
        var methods = new List<MethodInfo>();
        methods.AddRange(scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return methods;
    }

    private static List<Type> GetTypesInNamespace(string namespaceName)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Namespace == namespaceName)
            .ToList();
    }
    public static List<FieldInfo> Filter(this List<FieldInfo> list, string filterText)
    {
        return string.IsNullOrEmpty(filterText) ? list : list.Where(x => 
            x.Name.ToLower().Contains(filterText.ToLower()) ||
            x.FieldType.ToString().ToLower().Contains(filterText.ToLower())
        ).ToList();
    }
    public static List<PropertyInfo> Filter(this List<PropertyInfo> list, string filterText)
    {
        return string.IsNullOrEmpty(filterText) ? list : list.Where(x => 
            x.Name.ToLower().Contains(filterText.ToLower()) ||
            x.PropertyType.ToString().ToLower().Contains(filterText.ToLower())
        ).ToList();
    }
    public static List<MethodInfo> Filter(this List<MethodInfo> list, string filterText)
    {
        return string.IsNullOrEmpty(filterText) ? list : list.Where(x => 
            x.Name.ToLower().Contains(filterText.ToLower()) ||
            x.ReturnType.ToString().ToLower().Contains(filterText.ToLower())
        ).ToList();
    }
}
