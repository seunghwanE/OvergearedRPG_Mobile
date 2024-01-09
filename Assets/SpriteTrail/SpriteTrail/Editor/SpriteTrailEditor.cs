using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Trail2D
{
	[CustomEditor(typeof(SpriteTrail))]
	public class SpriteTrailEditor: TrailPropertyAssetEditor
	{
		private bool m_ShowGlobals = false;

		public override void OnInspectorGUI()
		{
			DrawDefult();
			DrawOthers();
			DrawGlobals();
			serializedObject.ApplyModifiedProperties();
		}

		void DrawGlobals()
		{
			m_ShowGlobals = EditorGUILayout.Foldout(m_ShowGlobals, "Global Settings", true);
			if(m_ShowGlobals)
			{
				TrailSettings asset = AssetDatabase.LoadAssetAtPath<TrailSettings>(TrailSettings.kPath);
				if(asset == null)
					asset = new TrailSettings();

				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.Toggle("HideInHierachy", asset.hideInHierachy);

				EditorGUILayout.IntField("DefaultPoolCount", asset.defaultPoolCount);
				EditorGUI.EndDisabledGroup();

				if(GUILayout.Button("Open Settings"))
				{
					TrailSettingsEditor.SelectSettings();
				}
			}
		}

		void DrawOthers()
		{
			SerializedProperty playOnAwake = serializedObject.FindProperty("m_PlayOnAwake");
			SerializedProperty removeTrailOnDisabled = serializedObject.FindProperty("m_RemoveTrailOnDisabled");

			EditorTools.BeginGroupBox("Others");

			playOnAwake.boolValue = EditorGUILayout.Toggle("Play On Awake", playOnAwake.boolValue);
			removeTrailOnDisabled.boolValue = EditorGUILayout.Toggle("Remove On Disabled", removeTrailOnDisabled.boolValue);

			EditorTools.EndGroupBox();
		}

		void DrawDefult()
		{
			SerializedProperty propertyAsset = serializedObject.FindProperty("m_PropertyAsset");
			SerializedProperty properties = serializedObject.FindProperty("m_Properties");

			EditorTools.BeginGroupBox("Properties");
			if(propertyAsset.objectReferenceValue == null)
			{
				DrawProperties(properties);
				EditorGUILayout.Space();
				DrawExportProperties();
			}

			propertyAsset.objectReferenceValue = EditorGUILayout.ObjectField("Property Asset", propertyAsset.objectReferenceValue, typeof(TrailPropertyAsset), false);

			EditorTools.EndGroupBox();
		}

		void DrawExportProperties()
		{
			if(GUILayout.Button("Export Properties", GUILayout.Height(30)))
			{
				TrailPropertyAsset asset = ScriptableObjectUtility.CreateAssetFilePanel<TrailPropertyAsset>("trail properties.asset");
				if(asset != null)
				{
					TrailProperties properties = ((SpriteTrail)serializedObject.targetObject).properties;
					asset.properties = new TrailProperties(properties);
				}
			}
		}
	}
}