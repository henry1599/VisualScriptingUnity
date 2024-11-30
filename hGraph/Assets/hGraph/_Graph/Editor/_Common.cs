using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AYellowpaper.SerializedCollections;

[Serializable]
public class ParsedField
{
    public string Name;
    public string Type;
}
[Serializable]
public class ParsedMethod
{
    public string Name;
    public string ReturnType;
    public List<ParsedField> Params;
    public string Content;
}
[Serializable]
public class ParsedMethodList
{
    public List<ParsedMethod> Data = new();
    public ParsedMethodList() => Data = new();
}
[Serializable]
public class ParsedFieldList
{
    public List<ParsedField> Data;
    public ParsedFieldList() => Data = new();
}
[Serializable]
public class ParsedScript
{
    public string Name;
    public string Path;
    public string Content;
    public List<string> ClassNames = new List<string>();
    [SerializedDictionary("ClassNames", "ClassMethods")]
    public SerializedDictionary<string, ParsedMethodList> ClassMethods;
    [SerializedDictionary("ClassNames", "ClassFields")]
    public SerializedDictionary<string, ParsedFieldList> ClassFields;
    public List<string> Namespaces = new List<string>();
    public List<string> UsingDirectives = new List<string>();
    public ParsedScript()
    {
        this.ClassMethods = new ();
        this.ClassFields = new ();
    }
    public static ParsedScript Create(string path)
    {
        ParsedScript parsedScript = new ParsedScript
        {
            Path = path,
            Name = System.IO.Path.GetFileNameWithoutExtension(path),
            Content = File.ReadAllText(path)
        };
        SyntaxTree tree = CSharpSyntaxTree.ParseText(parsedScript.Content);
        var root = tree.GetCompilationUnitRoot();

        // Clear previous data
        parsedScript.ClassNames.Clear();
        parsedScript.ClassMethods.Clear();
        parsedScript.ClassFields.Clear();
        parsedScript.Namespaces.Clear();
        parsedScript.UsingDirectives.Clear();

         // Extract using directives
        foreach (var usingDirective in root.DescendantNodes().OfType<UsingDirectiveSyntax>())
        {
            parsedScript.UsingDirectives.Add(usingDirective.Name.ToString());
        }

        // Extract namespaces
        foreach (var namespaceNode in root.DescendantNodes().OfType<NamespaceDeclarationSyntax>())
        {
            parsedScript.Namespaces.Add(namespaceNode.Name.ToString());
        }

        foreach (var classNode in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
        {
            string className = classNode.Identifier.Text;
            parsedScript.ClassNames.Add(className);
            parsedScript.ClassMethods.TryAdd(className, new());
            parsedScript.ClassFields.TryAdd(className, new());

            // Extract methods
            foreach (var methodNode in classNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methodName = methodNode.Identifier.Text;
                ParsedMethod parsedMethod = new ParsedMethod
                {
                    Name = methodName,
                    ReturnType = methodNode.ReturnType.ToString(),
                    Params = new List<ParsedField>(),
                    Content = methodNode.ToString()
                };
                // Extract parameters
                foreach (var paramNode in methodNode.ParameterList.Parameters)
                {
                    string paramName = paramNode.Identifier.Text;

                    ParsedField paramField = new ParsedField
                    {
                        Name = paramName,
                        Type = paramNode.Type.ToString()
                    };

                    parsedMethod.Params.Add(paramField);
                }
                parsedScript.ClassMethods[className].Data.Add(parsedMethod);


            }

            // Extract fields
            foreach (var fieldNode in classNode.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                string fieldType = fieldNode.Declaration.Type.ToString();
                foreach (var variable in fieldNode.Declaration.Variables)
                {
                    ParsedField parsedField = new ParsedField
                    {
                        Name = variable.Identifier.Text,
                        Type = fieldNode.Declaration.Type.ToString()
                    };
                    parsedScript.ClassFields[className].Data.Add(parsedField);

                }
            }
        }
        return parsedScript;
    }
}
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
    public static Button CreateButtonWithIcon(Texture icon, Button button)
    {
        VisualElement container = new VisualElement();
        container.style.flexDirection = FlexDirection.Row;
        Image iconImage = new Image() { image = icon };
        container.Add(iconImage);
        Label label = new Label(button.text);
        container.Add(label);
        button.text = string.Empty;
        container.style.maxHeight = 20f;
        button.Add(container);
        return button;
    }
    public static List<string> GetNamespaces(this Type scriptType)
    {
        var namespaces = new HashSet<string>();
        var allTypes = scriptType.Assembly.GetTypes();

        foreach (var type in allTypes)
        {
            if (type.Namespace != null && type.Namespace.StartsWith(scriptType.Namespace))
            {
                namespaces.Add(type.Namespace);
            }
        }

        return namespaces.ToList();
    }
    public static List<FieldInfo> GetFieldList(this Type scriptType, List<string> namespaces)
    {
        var fields = new List<FieldInfo>();
        fields.AddRange(scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return fields;
    }
    public static List<FieldInfo> GetMinimalFieldList(this Type scriptType, List<string> namespaces)
    {
        var fields = new List<FieldInfo>();
        var allFields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        var baseFields = scriptType.BaseType?.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) ?? new FieldInfo[0];

        foreach (var field in allFields)
        {
            if (field.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
            {
                continue;
            }
            if (!baseFields.Any(baseField => baseField.Name == field.Name && baseField.FieldType == field.FieldType))
            {
                fields.Add(field);
            }
        }

        return fields;
    }
    public static List<PropertyInfo> GetPropertyList(this Type scriptType, List<string> namespaces)
    {
        var properties = new List<PropertyInfo>();
        properties.AddRange(scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return properties;
    }
    public static List<PropertyInfo> GetMinimalPropertyList(this Type scriptType, List<string> namespaces)
    {
        var properties = new List<PropertyInfo>();
        var allProperties = scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        var baseProperties = scriptType.BaseType?.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) ?? new PropertyInfo[0];

        foreach (var property in allProperties)
        {
            if (!baseProperties.Any(baseProperty => baseProperty.Name == property.Name && baseProperty.PropertyType == property.PropertyType))
            {
                properties.Add(property);
            }
        }

        return properties;
    }

    public static List<MethodInfo> GetMethodList(this Type scriptType, List<string> namespaces)
    {
        var methods = new List<MethodInfo>();
        methods.AddRange(scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        return methods;
    }
    public static List<MethodInfo> GetMinimalMethodList(this Type scriptType, List<string> namespaces)
    {
        var methods = new List<MethodInfo>();
        var allMethods = scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        var baseMethods = scriptType.BaseType?.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) ?? new MethodInfo[0];

        foreach (var method in allMethods)
        {
            if (method.IsSpecialName && method.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
            {
                continue;
            }
            if (!baseMethods.Any(baseMethod => baseMethod.Name == method.Name && baseMethod.GetParameters().Select(p => p.ParameterType).SequenceEqual(method.GetParameters().Select(p => p.ParameterType))))
            {
                methods.Add(method);
            }
        }

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
    public static string Empty(int space)
    {
        string empty = string.Empty;
        for (int i = 0; i < space; i++)
        {
            empty += " ";
        }
        return empty;
    }
}
