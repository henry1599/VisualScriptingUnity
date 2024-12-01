using System.Collections;
using Sherbert.Framework.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
public class SerializableDictionaryDrawer : OdinValueDrawer<SerializableDictionary<object, object>>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var dictionary = this.ValueEntry.WeakSmartValue as IDictionary;
        if (dictionary == null)
        {
            SirenixEditorGUI.ErrorMessageBox("Dictionary is null.");
            return;
        }

        foreach (DictionaryEntry entry in dictionary)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(entry.Key.ToString(), GUILayout.Width(100));
            EditorGUILayout.LabelField(entry.Value.ToString(), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }
    }
}