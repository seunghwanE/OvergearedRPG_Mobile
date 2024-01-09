using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace Trail2D
{
	public class EditorTools
	{
		public static void BeginGroupBox()
		{
			EditorGUILayout.BeginVertical("Box");
		}

		public static void BeginGroupBox(string name)
		{
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
		}

		public static bool BeginToggleGroupBox(string name, SerializedProperty isChecked)
		{
			EditorGUILayout.BeginVertical("Box");

			EditorGUILayout.BeginHorizontal();
			isChecked.boolValue = EditorGUILayout.Toggle(isChecked.boolValue);
			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
			EditorGUILayout.EndHorizontal();

			return isChecked.boolValue;
		}

		public static void EndGroupBox()
		{
			EditorGUILayout.EndVertical();
		}

		public static void EndFoldBox()
		{
			EditorGUILayout.EndVertical();
		}

		public static int DrawSortingLayer(string labelName, int sortingLayerIndex)
		{
			// Look up the layer name using the current layer ID
			string oldName = SortingLayer.IDToName(sortingLayerIndex);

			var sortingLayerNames = SortingLayer.layers.Select(l => l.name).ToArray();

			// Use the name to look up our array index into the names list
			int oldLayerIndex = Array.IndexOf(sortingLayerNames, oldName);

			// Show the popup for the names
			int newLayerIndex = EditorGUILayout.Popup(labelName, oldLayerIndex, sortingLayerNames);

			return SortingLayer.NameToID(sortingLayerNames[newLayerIndex]);
		}
	}
}