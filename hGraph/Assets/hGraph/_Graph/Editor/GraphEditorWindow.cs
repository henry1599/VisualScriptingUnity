﻿using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BlueGraph.Editor
{
    /// <summary>
    /// Build a basic window container for the BlueGraph canvas
    /// </summary>
    public class GraphEditorWindow : EditorWindow
    {
        public VisualTreeAsset graphViewTreeAsset;
        private VisualElement mainLayout;
        public CanvasView Canvas { get; protected set; }
        public Graph Graph { get; protected set; }
        public static GraphEditorWindow window;

        // * Visual Elements
        private VisualElement graphViewContainer; 
        private VisualElement toolboxVisualElement;
        private ScrollView toolboxViewContainer;
        private ListView namespaceListView;
        private ToolbarSearchField toolbarSearchField;
        private ToolbarBreadcrumbs toolbarBreadcrumbs;

        public static void OpenGraphEditorWindow(Graph graph)
        {
            
            window = GetWindow<GraphEditorWindow>("Graph Editor"); 
            window.minSize = new Vector2(800, 600);
            window.Initialize(graph);
        }
        private void Initialize(Graph graph)
        {
            if (this.graphViewTreeAsset == null)
                return;
            
            this.Graph = graph;
            VisualElement root = rootVisualElement;
            graphViewTreeAsset.CloneTree(root);

            this.graphViewContainer = root.Q<VisualElement>("_graphField");
            this.toolboxViewContainer = root.Q<ScrollView>("_toolBoxContainer");
            this.toolboxVisualElement = root.Q<VisualElement>("_toolBox");
            this.mainLayout = root.Q<VisualElement>("_mainLayout");
            this.namespaceListView = root.Q<ListView>("_namespaceListView");
            this.toolbarSearchField = root.Q<ToolbarSearchField>("_toolbarSearchField");
            this.toolbarBreadcrumbs = root.Q<ToolbarBreadcrumbs>("_toolbarBreadCrumbs");

            TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            splitView.Add(toolboxVisualElement);
            splitView.Add(graphViewContainer);
            mainLayout.Add(splitView);
            splitView.StretchToParentSize();

            Load(this.Graph);
        }

        public virtual void Load(Graph graph)
        {
            Graph = graph;

            Canvas = new CanvasView(this);
            Canvas.Load(graph);
            this.graphViewContainer.Add(Canvas);
            Canvas.StretchToParentSize();

            titleContent = new GUIContent(graph.name);
        }
    }
}
