using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Trail2D
{
	[CustomEditor(typeof(TrailSettings))]
	public class TrailSettingsEditor: Editor
	{
		[MenuItem("Edit/Project Settings/Sprite Trail")]
		public static void SelectSettings()
		{
			TrailSettings asset = AssetDatabase.LoadAssetAtPath<TrailSettings>(TrailSettings.kPath);
			if(asset == null)
			{
				if(!AssetDatabase.IsValidFolder("Assets/SpriteTrail/Resources"))
				{
					AssetDatabase.CreateFolder("Assets/SpriteTrail", "Resources");
				}

				asset = CreateSettings();
			}

			Selection.activeObject = asset;
		}

		public static TrailSettings CreateSettings()
		{
			return ScriptableObjectUtility.CreateAsset<TrailSettings>(TrailSettings.kPath);
		}

		public override void OnInspectorGUI()
		{
			//SerializedProperty dontDestroyPool = serializedObject.FindProperty("dontDestroyPool");
			SerializedProperty hideInHierachy = serializedObject.FindProperty("hideInHierachy");
			SerializedProperty defaultPoolCount = serializedObject.FindProperty("defaultPoolCount");

			hideInHierachy.boolValue = EditorGUILayout.Toggle("HideInHierachy", hideInHierachy.boolValue);

			defaultPoolCount.intValue = EditorGUILayout.IntField("DefaultPoolCount", defaultPoolCount.intValue);

			serializedObject.ApplyModifiedProperties();
		}
	}
}