using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class EditorNodeDatabase : EditorWindow
{
    static EditorNodeDatabase window;
    GUIStyle titleStyle;
    string namespaceName = "";
    string className = "";

    [MenuItem("hGraph/Node Database")]
    public static void ShowWindow()
    {
        window = GetWindow<EditorNodeDatabase>("Node Database");
        window.minSize = new Vector2(400, 400);
    }

    private void OnGUI()
    {
        titleStyle ??= new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter
        };

        GUILayout.Label("Node Database Generator", titleStyle);
        GUILayout.Space(20);

        namespaceName = EditorGUILayout.TextField("Namespace", namespaceName);
        className = EditorGUILayout.TextField("Class Name", className);

        if (GUILayout.Button("Generate Nodes"))
        {
            GenerateNodes();
        }
    }

    private void GenerateNodes()
    {
        if (string.IsNullOrEmpty(namespaceName) || string.IsNullOrEmpty(className))
        {
            Debug.LogError("Namespace and Class Name must be specified!");
            return;
        }

        try
        {
            Type targetType = Type.GetType($"{namespaceName}.{className}, UnityEngine");
            if (targetType == null)
            {
                Debug.LogError("Class not found. Ensure the namespace and class name are correct.");
                return;
            }

            // Create a folder to store the generated nodes
            string path = EditorUtility.OpenFolderPanel("Choose Save Location", "Assets", "");
            if (string.IsNullOrEmpty(path))
                return;

            GenerateFieldNodes(targetType, path);
            GenerateMethodNodes(targetType, path);

            AssetDatabase.Refresh();
            Debug.Log("Node scripts generated successfully!");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error generating nodes: {ex.Message}");
        }
    }

    private void GenerateFieldNodes(Type targetType, string path)
    {
        foreach (var field in targetType.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string className = $"{field.Name}Node";
            string scriptContent = CreateNodeTemplate(className, targetType.Name, field.FieldType.Name, field.Name);
            File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
        }
    }

private void GenerateMethodNodes(Type targetType, string path)
{
    var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(m => !m.IsSpecialName &&
                                        !m.Name.StartsWith("get_") &&
                                        !m.Name.StartsWith("set_") &&
                                        !m.Name.StartsWith("GetComponent") &&
                                        !m.Name.StartsWith("TryGetComponent"));
    // A HashSet to store namespaces to avoid duplicates
    HashSet<string> namespaces = new HashSet<string>
    {
        "BlueGraph",  // Always include BlueGraph
        "Object = UnityEngine.Object", // Include UnityEngine.Object alias
        targetType.Namespace // Include the current class's namespace
    };
    
    foreach (var methodGroup in methods.GroupBy(m => m.Name))
    {
        int overloadIndex = 0;

        foreach (var method in methodGroup)
        {
            overloadIndex++;
            string className = $"{method.Name}Node{overloadIndex}";

            var parameters = method.GetParameters();
            string instanceCaller = $"[Input(Name = \"{targetType.Name}\")] public {targetType.Name} {targetType.Name.ToLower()};";
            string inputParams = string.Join("\n    ", parameters.Select(p =>
            {
                string paramType = p.ParameterType.IsByRef ? p.ParameterType.GetElementType().Name : p.ParameterType.Name;
                namespaces.Add(p.ParameterType.Namespace); // Add the parameter type's namespace
                return $"[Input(Name = \"{p.Name}\", Editable = true)] public {paramType} {p.Name};";
            }));

            // Add the return type's namespace if needed
            namespaces.Add(method.ReturnType.Namespace);

            // Determine the output node definition and return type handling
            string outputParams;
            string returnTypeHandling;

            if (method.ReturnType == typeof(void))
            {
                outputParams = "[Output] public Node exit;";
                returnTypeHandling = "exit";
            }
            else
            {
                outputParams = $"[Output(Name = \"result\")] public {method.ReturnType.Name} result;";
                returnTypeHandling = "result";
            }

            string methodCallParams = string.Join(", ", parameters.Select(p =>
            {
                if (p.IsOut) return $"out {p.Name}";
                if (p.ParameterType.IsByRef) return $"ref {p.Name}";
                return p.Name;
            }));

            // Method invocation string based on return type
            string methodInvocation = method.ReturnType == typeof(void)
                ? $"{targetType.Name.ToLower()}.{method.Name}({methodCallParams});"
                : $"{method.ReturnType.Name} result = {targetType.Name.ToLower()}.{method.Name}({methodCallParams});";
            // Create the using directives
            string usingDirectives = string.Join("\n", namespaces.Distinct().Select(ns => $"using {ns};"));
            string scriptContent = $@"
{usingDirectives}

[Node(
    Name = ""{method.Name} Overload {overloadIndex}"",
    Path = ""{targetType.Namespace}/{targetType.Name}/{method.Name}"",
    Deletable = true,
    Help = ""{method.Name} overload {overloadIndex} of {targetType.Name}""
)]
public class {className} : Node
{{
    [Input] public Node entry;
    {instanceCaller}
    {inputParams}

    {outputParams}

    public override object OnRequestValue(Port port)
    {{
        {methodInvocation}
        return {returnTypeHandling};
    }}
}}";

            File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
        }
    }
}







    private string CreateNodeTemplate(string nodeName, string className, string fieldType, string fieldName)
    {
        return $@"using BlueGraph;
using {namespaceName};
using System;
using Object = UnityEngine.Object;

[Node(
    Name = ""{fieldName}"",
    Path = ""{namespaceName}/{className}/Fields/{fieldName}"",
    Deletable = true,
    Help = ""{fieldName} of {className}""
)]
public class {nodeName} : Node
{{
    [Input] public Node entry;
    [Output] public Node exit;

    [Input(Name = ""{className}"")] public {className} {className.ToLower()};
    [Output(Name = ""{fieldName}"")] public {fieldType} {fieldName.ToLower()};

    public override object OnRequestValue(Port port)
    {{
        return {className.ToLower()}.{fieldName};
    }}
}}";
    }
}
