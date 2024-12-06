﻿using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MaxyGames.UNode.Editors.Drawer {
	class UIntPropertyDrawer : UPropertyDrawer<uint> {
		public override void Draw(Rect position, DrawerOption option) {
			EditorGUI.BeginChangeCheck();
			var fieldValue = GetValue(option.property);
			var att = ReflectionUtils.GetAttribute<RangeAttribute>(option.property.GetCustomAttributes());
			if(att != null) {
				fieldValue = (uint)EditorGUI.IntSlider(position, option.label, (int)fieldValue, (int)att.min, (int)att.max);
			} else {
				fieldValue = (uint)EditorGUI.DelayedIntField(position, option.label, (int)fieldValue);
			}
			if(EditorGUI.EndChangeCheck()) {
				option.value = fieldValue;
			}
		}
	}
}