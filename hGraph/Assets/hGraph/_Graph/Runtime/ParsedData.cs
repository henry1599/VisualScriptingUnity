using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sherbert.Framework.Generic;
using Sirenix.OdinInspector;
using System.Collections;


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
    public List<ParsedMethod> Data;
    public ParsedMethodList()
    {
        this.Data = new List<ParsedMethod>();
    }
}
[Serializable]
public class ParsedFieldList
{
    public List<ParsedField> Data;
    public ParsedFieldList()
    {
        this.Data = new List<ParsedField>();
    }
}
[Serializable]
public class MethodDict : UnitySerializedDictionary<string, ParsedMethodList> { }
[Serializable]
public class FieldDict : UnitySerializedDictionary<string, ParsedFieldList> { }
[Serializable]
public class ParsedScript
{
    public string Name;
    public string Path;
    public string Content;
    public List<string> ClassNames = new List<string>();
    [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)] public MethodDict ClassMethods;
    [SerializeField, DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)] public FieldDict ClassFields;
    public List<string> Namespaces = new List<string>();
    public List<string> UsingDirectives = new List<string>();
    public List<string> AllNamespaces => Namespaces.Concat(UsingDirectives).ToList();
    public ParsedScript()
    {
        this.ClassMethods = new ();
        this.ClassFields =  new ();
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