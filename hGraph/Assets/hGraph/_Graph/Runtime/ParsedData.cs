using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sirenix.OdinInspector;

public enum eParsedDataType
{
    Unknown = 0,
    Class,
    Method,
    Field
}
[Serializable]
public abstract class ParsedObject
{
    public abstract eParsedDataType Category {get;}
    public ParsedObject Parent;
    public string Name;
    public string RootNamespace;
    public string Type;
    public override bool Equals(object obj)
    {
        return obj is ParsedObject @object &&
               string.Equals(Name, @object.Name, StringComparison.Ordinal) &&
               string.Equals(RootNamespace, @object.RootNamespace, StringComparison.Ordinal) &&
               string.Equals(Type, @object.Type, StringComparison.Ordinal);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, RootNamespace, Type);
    }
}
[Serializable]
public class ParsedField : ParsedObject
{
    [ShowInInspector] public override eParsedDataType Category => eParsedDataType.Field;
    public override string ToString()
    {
        string parentName = Parent == null ? "Global" : $"{Parent.Type} {Parent.Name}";
        return $"{RootNamespace} <- {parentName} <- {Type} <- {Name}";
    }
}
[Serializable]
public class ParsedMethod : ParsedObject
{
    [ShowInInspector] public override eParsedDataType Category => eParsedDataType.Method;
    public string ReturnType => Type;
    public List<ParsedField> Params;
    [FoldoutGroup("Content"), TextArea(10, 20), ReadOnly] public string Content; // Makes the string field bigger and scrollable
    public override string ToString()
    {
        string parentName = Parent == null ? "Global" : $"{Parent.Type} {Parent.Name}";
        return $"{RootNamespace} <- {parentName} <- {ReturnType} <- {Name}({string.Join(", ", Params.Select(p => $"{p.Type} {p.Name}"))})";
    }
}
[Serializable]
public class ParsedClass : ParsedObject
{
    [ShowInInspector] public override eParsedDataType Category => eParsedDataType.Class;
    [TabGroup("ParsedClass", "Methods")]
    public List<ParsedMethod> Methods;
    [TabGroup("ParsedClass", "Fields")]
    public List<ParsedField> Fields;
    public ParsedClass()
    {
        this.Methods = new List<ParsedMethod>();
        this.Fields = new List<ParsedField>();
    }
    public override string ToString()
    {
        return $"{RootNamespace} <- {Name}";
    }

}
[Serializable]
public class ClassDict : UnitySerializedDictionary<string, ParsedClass> { }
[Serializable]
public class ParsedScript
{
    public string Name;
    public string Path;
    [FoldoutGroup("Content"), TextArea(10, 20), ReadOnly] public string Content;
    
    [SerializeField] 
    [TabGroup("ParsedScript", "Classes")]
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)] 
    public ClassDict Classes = new ();
    [TabGroup("ParsedScript", "Namespaces")]
    public List<string> Namespaces = new List<string>();
    [TabGroup("ParsedScript", "Namespaces")]
    public List<string> UsingDirectives = new List<string>();
    public List<string> AllNamespaces => Namespaces.Concat(UsingDirectives).ToList();
    public ParsedScript()
    {
        this.Classes = new ClassDict();
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
        parsedScript.Classes.Clear();
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
            parsedScript.Classes.TryAdd(className, new ParsedClass()
            {
                Name = className,
                Type = "<class>",
                RootNamespace = classNode.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString()
            });

            // Extract methods
            foreach (var methodNode in classNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methodName = methodNode.Identifier.Text;
                string rootNamespaceMethod = methodNode.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString();
                ParsedMethod parsedMethod = new ParsedMethod
                {
                    Parent = parsedScript.Classes[className],
                    Name = methodName,
                    Type = methodNode.ReturnType.ToString(),
                    RootNamespace = rootNamespaceMethod,
                    Params = new List<ParsedField>(),
                    Content = methodNode.ToString()
                };
                // Extract parameters
                foreach (var paramNode in methodNode.ParameterList.Parameters)
                {
                    string paramName = paramNode.Identifier.Text;
                    ParsedField paramField = new ParsedField
                    {
                        Parent = parsedMethod,
                        Name = paramName,
                        RootNamespace = rootNamespaceMethod,
                        Type = paramNode.Type.ToString()
                    };

                    parsedMethod.Params.Add(paramField);
                }
                parsedScript.Classes[className].Methods.Add(parsedMethod);


            }

            // Extract fields
            foreach (var fieldNode in classNode.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                string rootNamespaceField = fieldNode.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString();
                foreach (var variable in fieldNode.Declaration.Variables)
                {
                    ParsedField parsedField = new ParsedField
                    {
                        Parent = parsedScript.Classes[className],
                        Name = variable.Identifier.Text,
                        RootNamespace = rootNamespaceField,
                        Type = fieldNode.Declaration.Type.ToString()
                    };
                    parsedScript.Classes[className].Fields.Add(parsedField);

                }
            }
        }
        return parsedScript;
    }
}