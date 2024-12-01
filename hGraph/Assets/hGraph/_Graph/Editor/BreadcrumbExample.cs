using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class BreadcrumbExampleWindow : EditorWindow
{
    [MenuItem("Window/ToolbarBreadcrumbs Example")]
    public static void ShowExample()
    {
        BreadcrumbExampleWindow wnd = GetWindow<BreadcrumbExampleWindow>();
        wnd.titleContent = new GUIContent("ToolbarBreadcrumbs");
    }

    public void CreateGUI()
    {
        // Create the root VisualElement
        VisualElement root = rootVisualElement;

        // Create a ToolbarBreadcrumbs instance
        ToolbarBreadcrumbs breadcrumbs = new ToolbarBreadcrumbs();

        // Add breadcrumb items
        breadcrumbs.PushItem("Home", () => OnBreadcrumbClicked("Home"));
        breadcrumbs.PushItem("Settings", () => OnBreadcrumbClicked("Settings"));
        breadcrumbs.PushItem("Advanced", () => OnBreadcrumbClicked("Advanced"));

        // Add breadcrumbs to the UI
        root.Add(breadcrumbs);
    }

    private void OnBreadcrumbClicked(string itemName)
    {
        Debug.Log($"Breadcrumb clicked: {itemName}");
        // Perform different actions based on the item clicked
        switch (itemName)
        {
            case "Home":
                Debug.Log("Navigate to Home");
                break;
            case "Settings":
                Debug.Log("Navigate to Settings");
                break;
            case "Advanced":
                Debug.Log("Navigate to Advanced");
                break;
        }
    }
}
