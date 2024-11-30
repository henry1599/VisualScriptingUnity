using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System;

public class RoslynScriptAnalyzer : EditorWindow
{
    private string filePath = "";
    private List<string> classNames = new List<string>();
    private Dictionary<string, List<string>> classMethods = new Dictionary<string, List<string>>();
    private Dictionary<string, List<string>> classFields = new Dictionary<string, List<string>>();
    private Vector2 scrollPos;
    private MonoScript script;

    [MenuItem("Tools/Roslyn Script Analyzer")]
    public static void ShowWindow()
    {
        GetWindow<RoslynScriptAnalyzer>("Roslyn Script Analyzer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Analyze C# Script", EditorStyles.boldLabel);

        this.script = EditorGUILayout.ObjectField("Script:", this.script, typeof(MonoScript), false) as MonoScript;
        if (script != null)
        {
            filePath = AssetDatabase.GetAssetPath(script);
        }


        if (GUILayout.Button("Analyze"))
        {
            if (File.Exists(this.filePath))
            {
                AnalyzeScript(this.filePath);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "File not found!", "OK");
            }
        }

        scrollPos = GUILayout.BeginScrollView(scrollPos);

        if (classNames.Count > 0)
        {
            foreach (var className in classNames)
            {
                GUILayout.Label($"Class: {className}", EditorStyles.boldLabel);

                GUILayout.Label("Methods:", EditorStyles.miniBoldLabel);
                if (classMethods.ContainsKey(className))
                {
                    foreach (var method in classMethods[className])
                    {
                        GUILayout.Label($"- {method}", EditorStyles.boldLabel);
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
        GUILayout.EndScrollView();
    }

    private void AnalyzeScript(string path)
    {
        ParsedScript parsedScript = ParsedScript.Create(path);

        // Clear previous data
        classNames.Clear();
        classMethods.Clear();
        classFields.Clear();

        foreach (var classNode in parsedScript.ClassNames)
        {
            string className = classNode;
            classNames.Add(className);

            classMethods[className] = new List<string>();
            classFields[className] = new List<string>();

            // Extract methods
            foreach (var methodNode in parsedScript.ClassMethods[className].Data)
            {
                List<string> parameters = new();
                foreach (var param in methodNode.Params)
                {
                    parameters.Add($"{ShortenType(param.Type)} {param.Name}");
                }
                string parametersString = string.Join(", ", parameters);
                string methodName = $"{ShortenType(methodNode.ReturnType)} {methodNode.Name}({parametersString})";
                classMethods[className].Add(methodName);
            }

            // Extract fields
            foreach (var fieldNode in parsedScript.ClassFields[className].Data)
            {
                string fieldType = fieldNode.Type.ToString();
                classFields[className].Add($"{ShortenType(fieldType)} {fieldNode.Name}");
            }
        }
    }
    private string ShortenType(Type type)
    {
        return ShortenType(type.ToString());
    }
    private string ShortenType(string type)
    {
        return type.Split(".")[^1];
    }
}
