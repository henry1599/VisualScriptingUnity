﻿using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaxyGames.UNode.Editors.Control {
	class Color32FieldControl : FieldControl<Color32> {
		public override void Draw(Rect position, GUIContent label, object value, Type type, Action<object> onChanged, uNodeUtility.EditValueSettings settings) {
			EditorGUI.BeginChangeCheck();
			ValidateValue(ref value);
			var oldValue = (Color32)value;
			var newValue = (Color32)EditorGUI.ColorField(position, label, oldValue);
			if(EditorGUI.EndChangeCheck()) {
				onChanged(newValue);
			}
		}
	}
}