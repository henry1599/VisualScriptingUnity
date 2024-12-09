﻿using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MaxyGames.UNode.Editors.Drawer {
	class Color32PropertyDrawer : UPropertyDrawer<Color32> {
		public override void Draw(Rect position, DrawerOption option) {
			EditorGUI.BeginChangeCheck();
			var fieldValue = GetValue(option.property);
			var att = ReflectionUtils.GetAttribute<ColorUsageAttribute>(option.property.GetCustomAttributes());
			if(att != null) {
				fieldValue = (Color32)EditorGUI.ColorField(position, option.label, fieldValue, true, att.showAlpha, att.hdr);
			} else {
				fieldValue = (Color32)EditorGUI.ColorField(position, option.label, fieldValue);
			}
			if(EditorGUI.EndChangeCheck()) {
				option.value = fieldValue;
			}
		}
	}
}