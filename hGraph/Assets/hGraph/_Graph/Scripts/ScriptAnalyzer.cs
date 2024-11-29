using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

public class RoslynScriptAnalyzer : EditorWindow
{
    private string filePath = "";
    private List<string> classNames = new List<string>();
    private Dictionary<string, List<string>> classMethods = new Dictionary<string, List<string>>();
    private Dictionary<string, List<string>> classFields = new Dictionary<string, List<string>>();

    [MenuItem("Tools/Roslyn Script Analyzer")]
    public static void ShowWindow()
    {
        GetWindow<RoslynScriptAnalyzer>("Roslyn Script Analyzer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Analyze C# Script", EditorStyles.boldLabel);

        filePath = EditorGUILayout.TextField("File Path:", filePath);

        if (GUILayout.Button("Analyze"))
        {
            if (File.Exists(filePath))
            {
                AnalyzeScript(filePath);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "File not found!", "OK");
            }
        }

        if (classNames.Count > 0)
        {
            GUILayout.Label("Classes Found:", EditorStyles.boldLabel);
            foreach (var className in classNames)
            {
                GUILayout.Label($"Class: {className}", EditorStyles.boldLabel);

                GUILayout.Label("Methods:", EditorStyles.miniBoldLabel);
                if (classMethods.ContainsKey(className))
                {
                    foreach (var method in classMethods[className])
                    {
                        GUILayout.Label($"- {method}");
                    }
                }

                GUILayout.Label("Fields:", EditorStyles.miniBoldLabel);
                if (classFields.ContainsKey(className))
                {
                    foreach (var field in classFields[className])
                    {
                        GUILayout.Label($"- {field}");
                    }
                }
            }
        }
    }

    private void AnalyzeScript(string path)
    {
        string code = File.ReadAllText(path);

        // Parse the code into a syntax tree
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetCompilationUnitRoot();

        // Clear previous data
        classNames.Clear();
        classMethods.Clear();
        classFields.Clear();

        // Extract classes
        foreach (var classNode in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
        {
            string className = classNode.Identifier.Text;
            classNames.Add(className);

            classMethods[className] = new List<string>();
            classFields[className] = new List<string>();

            // Extract methods
            foreach (var methodNode in classNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                classMethods[className].Add(methodNode.Identifier.Text);
            }

            // Extract fields
            foreach (var fieldNode in classNode.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                foreach (var variable in fieldNode.Declaration.Variables)
                {
                    classFields[className].Add(variable.Identifier.Text);
                }
            }
        }
    }
}
