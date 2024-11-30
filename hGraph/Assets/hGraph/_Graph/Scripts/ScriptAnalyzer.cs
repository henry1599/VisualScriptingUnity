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
    private Dictionary<string, Dictionary<string, List<string>>> methodDetails = new Dictionary<string, Dictionary<string, List<string>>>();
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

        // if (script != null)
        // {
        //     filePath = AssetDatabase.GetAssetPath(script);
        // }
        // filePath = EditorGUILayout.TextField("File Path:", filePath);

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

                        // Display local variables and functions
                        if (methodDetails[className].ContainsKey(method))
                        {
                            foreach (var detail in methodDetails[className][method])
                            {
                                GUILayout.Label($"  {detail}", EditorStyles.miniLabel);
                            }
                        }
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
        string code = File.ReadAllText(path);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetCompilationUnitRoot();

        // Clear previous data
        classNames.Clear();
        classMethods.Clear();
        classFields.Clear();
        methodDetails.Clear();

        foreach (var classNode in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
        {
            string className = classNode.Identifier.Text;
            classNames.Add(className);

            classMethods[className] = new List<string>();
            classFields[className] = new List<string>();
            methodDetails[className] = new Dictionary<string, List<string>>();

            // Extract methods
            foreach (var methodNode in classNode.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                string methodName = $"{methodNode.ReturnType} {methodNode.Identifier.Text}()";
                classMethods[className].Add(methodName);

                methodDetails[className][methodName] = new List<string>();

                // Extract local variables
                foreach (var localVar in methodNode.DescendantNodes().OfType<LocalDeclarationStatementSyntax>())
                {
                    var variable = localVar.Declaration.Variables.First();
                    string localVarType = localVar.Declaration.Type.ToString();
                    string localVarName = variable.Identifier.Text;
                    methodDetails[className][methodName].Add($"Local Var: {localVarType} {localVarName}");
                }

                // Extract local functions
                foreach (var localFunc in methodNode.DescendantNodes().OfType<LocalFunctionStatementSyntax>())
                {
                    string localFuncReturnType = localFunc.ReturnType.ToString();
                    string localFuncName = localFunc.Identifier.Text;
                    methodDetails[className][methodName].Add($"Local Func: {localFuncReturnType} {localFuncName}()");
                }
            }

            // Extract fields
            foreach (var fieldNode in classNode.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                string fieldType = fieldNode.Declaration.Type.ToString();
                foreach (var variable in fieldNode.Declaration.Variables)
                {
                    classFields[className].Add($"{fieldType} {variable.Identifier.Text}");
                }
            }
        }
    }
}
