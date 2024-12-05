using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class EditorNodeDatabase : EditorWindow
{
    static EditorNodeDatabase window;
    private static readonly string GENERATED_PATH = "Assets/hGraph/_Graph/Generated";
    GUIStyle titleStyle;
    string namespaceName = "";
    string className = "";
    string inputClassName = "";

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
            this.inputClassName = $"{className}_Generated";
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
            //string path = EditorUtility.OpenFolderPanel("Choose Save Location", "Assets", "");
            //if (string.IsNullOrEmpty(path))
            //    return;
            // Create the folder path to store the generated nodes
            string path = Path.Combine(GENERATED_PATH, namespaceName.Replace('.', '/'), className);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            GenerateFieldNodes(targetType, path);
            GeneratePropertyNodes(targetType, path);
            GenerateMethodNodes(targetType, path);
            GenerateStaticMethodNodes(targetType, path);

            AssetDatabase.Refresh();
            Debug.Log("Node scripts generated successfully!");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error generating nodes: {ex.Message}");
        }
    }

    private void GeneratePropertyNodes(Type targetType, string path)
    {
        var properties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .Where(p => (p.CanRead || p.CanWrite) && !p.IsDefined(typeof(ObsoleteAttribute), false));

        // A HashSet to store namespaces to avoid duplicates
        HashSet<string> namespaces = new HashSet<string>
        {
            "BlueGraph",  // Always include BlueGraph
            "Object = UnityEngine.Object", // Include UnityEngine.Object alias
            targetType.Namespace // Include the current class's namespace
        };

        foreach (var property in properties)
        {
            string propertyName = property.Name;
            string propertyType = property.PropertyType.Name;
            namespaces.Add(property.PropertyType.Namespace); // Add the property type's namespace
            string usingDirectives = string.Join("\n", namespaces.Distinct().Select(ns => $"using {ns};"));

            if (property.CanRead)
            {
                string className = $"{propertyName}GetterNode";
                string methodPath = $"{targetType.Namespace}/{targetType.Name}/Properties/{propertyName}";

                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
{{
    [Node(
        Name = ""Getter"",
        Path = ""{methodPath}"",
        Deletable = true,
        Help = ""Getter for {propertyName} of {targetType.Name}""
    )]
    public class {className} : Node
    {{
        [Input] public Node entry;
        [Input(Name = ""{targetType.Name}"")] public {targetType.Name} {targetType.Name.ToLower()};
        [Output(Name = ""{propertyName}"")] public {propertyType} {propertyName.ToLower()};

        public override object OnRequestValue(Port port)
        {{
            return {targetType.Name.ToLower()}.{propertyName};
        }}
    }}
}}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }

            if (property.CanWrite)
            {
                string className = $"{propertyName}SetterNode";
                string methodPath = $"{targetType.Namespace}/{targetType.Name}/Properties/{propertyName}";

                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
{{

    [Node(
        Name = ""Setter"",
        Path = ""{methodPath}"",
        Deletable = true,
        Help = ""Setter for {propertyName} of {targetType.Name}""
    )]
    public class {className} : Node
    {{
        [Input] public Node entry;
        [Input(Name = ""{targetType.Name}"")] public {targetType.Name} {targetType.Name.ToLower()};
        [Input(Name = ""{propertyName}"", Editable = true)] public {propertyType} {propertyName.ToLower()};
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {{
            {targetType.Name.ToLower()}.{propertyName} = {propertyName.ToLower()};
            return exit;
        }}
    }}
}}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }
        }
    }
    private void GenerateStaticMethodNodes(Type targetType, string path)
    {
        var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                .Where(m => !m.IsSpecialName &&
                                            !m.Name.StartsWith("get_") &&
                                            !m.Name.StartsWith("set_") &&
                                            !m.Name.StartsWith("GetComponent") &&
                                            !m.Name.StartsWith("TryGetComponent") &&
                                            !m.Name.StartsWith("ToString") &&
                                            !m.Name.StartsWith("Equals") &&
                                            !m.Name.StartsWith("GetHashCode") &&
                                            !m.Name.StartsWith("GetInstanceID") &&
                                            !m.Name.StartsWith("GetType") &&
                                            !m.Name.StartsWith("Clone") &&
                                            !m.Name.StartsWith("Finalize"));

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
                string inputParams = string.Join("\n    ", parameters.Select(p =>
                {
                    string paramType = GetFriendlyTypeName(p.ParameterType, p.IsOut);
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
                    outputParams = $"[Output(Name = \"result\")] public {GetFriendlyTypeName(method.ReturnType)} result;";
                    returnTypeHandling = "result";
                }

                string methodCallParams = GetMethodCallParams(parameters);

                // Create the method path with parameters
                string methodParams = string.Join(", ", parameters.Select(p => $"{GetFriendlyTypeName(p.ParameterType, p.IsOut)} {p.Name}"));
                string methodPath = parameters.Length == 0 ? $"{targetType.Namespace}/{targetType.Name}/Methods" : $"{targetType.Namespace}/{targetType.Name}/Methods/{method.Name}";

                string methodName = parameters.Length == 0 ? method.Name : $"{method.Name} ({methodParams})";

                // Method invocation string based on return type
                string methodInvocation = method.ReturnType == typeof(void)
                    ? $"{targetType.Name}.{method.Name}({methodCallParams});"
                    : $"{GetFriendlyTypeName(method.ReturnType)} result = {targetType.Name}.{method.Name}({methodCallParams});";

                // Create the using directives
                string usingDirectives = string.Join("\n", namespaces.Distinct().Select(ns => $"using {ns};"));
                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
    {{
        [Node(
            Name = ""(Static) {methodName}"",
            Path = ""{methodPath}"",
            Deletable = true,
            Help = ""{method.Name} overload {overloadIndex} of {targetType.Name}""
        )]
        public class {className} : Node
        {{
            [Input] public Node entry;
            {inputParams}

            {outputParams}

            public override object OnRequestValue(Port port)
            {{
                {methodInvocation}
                return {returnTypeHandling};
            }}
        }}
    }}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }
        }
    }
    private void GenerateMethodNodes(Type targetType, string path)
    {
        var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                .Where(m => !m.IsSpecialName &&
                                            !m.Name.StartsWith("get_") &&
                                            !m.Name.StartsWith("set_") &&
                                            !m.Name.StartsWith("GetComponent") &&
                                            !m.Name.StartsWith("TryGetComponent") &&
                                            !m.Name.StartsWith("ToString") &&
                                            !m.Name.StartsWith("Equals") &&
                                            !m.Name.StartsWith("GetHashCode") &&
                                            !m.Name.StartsWith("GetInstanceID") &&
                                            !m.Name.StartsWith("GetType") &&
                                            !m.Name.StartsWith("Clone") &&
                                            !m.Name.StartsWith("Finalize"));

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
                    string paramType = GetFriendlyTypeName(p.ParameterType, p.IsOut);
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
                    outputParams = $"[Output(Name = \"result\")] public {GetFriendlyTypeName(method.ReturnType)} result;";
                    returnTypeHandling = "result";
                }

                string methodCallParams = GetMethodCallParams(parameters);

                // Create the method path with parameters
                string methodParams = string.Join(", ", parameters.Select(p => $"{GetFriendlyTypeName(p.ParameterType, p.IsOut)} {p.Name}"));
                string methodPath = parameters.Length == 0 ? $"{targetType.Namespace}/{targetType.Name}/Methods" : $"{targetType.Namespace}/{targetType.Name}/Methods/{method.Name}";

                string methodName = parameters.Length == 0 ? method.Name : $"{method.Name} ({methodParams})";

                // Method invocation string based on return type
                string methodInvocation = method.ReturnType == typeof(void)
                    ? $"{targetType.Name.ToLower()}.{method.Name}({methodCallParams});"
                    : $"{GetFriendlyTypeName(method.ReturnType)} result = {targetType.Name.ToLower()}.{method.Name}({methodCallParams});";

                // Create the using directives
                string usingDirectives = string.Join("\n", namespaces.Distinct().Select(ns => $"using {ns};"));
                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
    {{
        [Node(
            Name = ""{methodName}"",
            Path = ""{methodPath}"",
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
        }}
    }}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }
        }
    }
    private string GetMethodCallParams(ParameterInfo[] parameters)
    {
        return string.Join(", ", parameters.Select(p =>
        {
            //if (p.IsDefined(typeof(OutAttribute), true)) return $"{p.Name}";
            //if (p.IsOut) return $"out {p.Name}";
            if (p.ParameterType.IsByRef && !p.IsOut) return $"ref {p.Name}";
            if (!p.ParameterType.IsByRef && p.IsOut) return $"{p.Name}";
            if (p.IsOut) return $"out {p.Name}";
            return p.Name;
        }));
    }
    private string GetFriendlyTypeName(Type type, bool isOut = false)
    {
        if (type.IsByRef)
        {
            type = type.GetElementType();
            return isOut ? $"{type.Name}" : $"{type.Name}";
        }

        if (type.IsGenericType)
        {
            string genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            string genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => GetFriendlyTypeName(t)));
            return $"{genericTypeName}<{genericArgs}>";
        }

        if (type.IsArray)
        {
            return $"{GetFriendlyTypeName(type.GetElementType())}[]";
        }

        return type.Name;
    }
    private void GenerateFieldNodes(Type targetType, string path)
    {
        var fields = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance)
                               .Where(f => !f.IsDefined(typeof(ObsoleteAttribute), false));

        // A HashSet to store namespaces to avoid duplicates
        HashSet<string> namespaces = new HashSet<string>
        {
            "BlueGraph",  // Always include BlueGraph
            "Object = UnityEngine.Object", // Include UnityEngine.Object alias
            targetType.Namespace // Include the current class's namespace
        };

        foreach (var field in fields)
        {
            string fieldName = field.Name;
            string fieldType = field.FieldType.Name;
            namespaces.Add(field.FieldType.Namespace); // Add the field type's namespace
            string usingDirectives = string.Join("\n", namespaces.Distinct().Select(ns => $"using {ns};"));

            // Generate getter node
            {
                string className = $"{fieldName}GetterNode";
                string methodPath = $"{targetType.Namespace}/{targetType.Name}/Fields/{fieldName}";

                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
{{

    [Node(
        Name = ""Getter"",
        Path = ""{methodPath}"",
        Deletable = true,
        Help = ""Getter for {fieldName} of {targetType.Name}""
    )]
    public class {className} : Node
    {{
        [Input] public Node entry;
        [Input(Name = ""{targetType.Name}"")] public {targetType.Name} {targetType.Name.ToLower()};
        [Output(Name = ""{fieldName}"")] public {fieldType} {fieldName.ToLower()};

        public override object OnRequestValue(Port port)
        {{
            return {targetType.Name.ToLower()}.{fieldName};
        }}
    }}
}}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }

            // Generate setter node
            {
                string className = $"{fieldName}SetterNode";
                string methodPath = $"{targetType.Namespace}/{targetType.Name}/Fields/{fieldName}";

                string scriptContent = $@"
    {usingDirectives}
    namespace CustomNode.{namespaceName}.{inputClassName}
{{

    [Node(
        Name = ""Setter"",
        Path = ""{methodPath}"",
        Deletable = true,
        Help = ""Setter for {fieldName} of {targetType.Name}""
    )]
    public class {className} : Node
    {{
        [Input] public Node entry;
        [Input(Name = ""{targetType.Name}"")] public {targetType.Name} {targetType.Name.ToLower()};
        [Input(Name = ""{fieldName}"", Editable = true)] public {fieldType} {fieldName.ToLower()};
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {{
            {targetType.Name.ToLower()}.{fieldName} = {fieldName.ToLower()};
            return exit;
        }}
    }}
}}";

                File.WriteAllText(Path.Combine(path, $"{className}.cs"), scriptContent);
            }
        }
    }
}
