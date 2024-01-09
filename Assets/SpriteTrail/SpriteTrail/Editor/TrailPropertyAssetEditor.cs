using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Trail2D
{
	[CustomEditor(typeof(TrailPropertyAsset))]
	public class TrailPropertyAssetEditor: Editor
	{
		public override void OnInspectorGUI()
		{
			SerializedProperty properties = serializedObject.FindProperty("properties");

			DrawProperties(properties);

			serializedObject.ApplyModifiedProperties();
		}

		protected void DrawProperties(SerializedProperty properties)
		{
			DrawTrailProperty(properties);
			DrawLifeTimeProperty(properties);
			DrawLifeColorProperty(properties);
			DrawScaleProperty(properties);
			DrawRenderProperty(properties);
			DrawMaterialProperty(properties);
		}

		void DrawTrailProperty(SerializedProperty properties)
		{
			SerializedProperty method = properties.FindPropertyRelative("emitMethod");

			EditorTools.BeginGroupBox();

			EmitMethod emitMethod = (EmitMethod)EditorGUILayout.EnumPopup("Method", (EmitMethod)method.intValue);
			method.intValue = (int)emitMethod;

			if(emitMethod == EmitMethod.Time)
			{
				SerializedProperty delay = properties.FindPropertyRelative("emitDelay");
				SerializedProperty onlyWhenMoved = properties.FindPropertyRelative("onlyWhenMoved");

				delay.floatValue = EditorGUILayout.FloatField("Interval", delay.floatValue);
				onlyWhenMoved.boolValue = EditorGUILayout.Toggle("Only When Moved", onlyWhenMoved.boolValue);
				EditorGUILayout.HelpBox("If checked, accumulates time only when the object is moved.", MessageType.Info);
			}
			else if(emitMethod == EmitMethod.Distance)
			{
				SerializedProperty distance = properties.FindPropertyRelative("emitDistance");

				distance.floatValue = EditorGUILayout.FloatField("Spacing", distance.floatValue);
			}

			EditorTools.EndGroupBox();
		}

		void DrawLifeTimeProperty(SerializedProperty properties)
		{
			SerializedProperty lifeTime = properties.FindPropertyRelative("lifeTime");

			EditorTools.BeginGroupBox("LifeTime");
			lifeTime.floatValue = EditorGUILayout.FloatField("LifeTime", lifeTime.floatValue);
			EditorTools.EndGroupBox();
		}

		void DrawLifeColorProperty(SerializedProperty properties)
		{
			SerializedProperty overrideColor = properties.FindPropertyRelative("overrideColor");
			;
			if(EditorTools.BeginToggleGroupBox("Color over LifeTime", overrideColor))
			{
				SerializedProperty lifeColorType = properties.FindPropertyRelative("lifeColorType");
				SerializedProperty lifeColor = properties.FindPropertyRelative("lifeColor");
				SerializedProperty alphaCurve = properties.FindPropertyRelative("alphaCurve");
				SerializedProperty lifeGradient = properties.FindPropertyRelative("lifeGradient");

				LifeColorType lifeColorEnum = (LifeColorType)EditorGUILayout.EnumPopup("Color Type", (LifeColorType)lifeColorType.intValue);
				lifeColorType.intValue = (int)lifeColorEnum;

				if(lifeColorEnum == LifeColorType.Solid)
				{
					lifeColor.colorValue = EditorGUILayout.ColorField("Color", lifeColor.colorValue);
					alphaCurve.animationCurveValue = EditorGUILayout.CurveField("Alpha Multiplier Curve", alphaCurve.animationCurveValue);
				}
				else if(lifeColorEnum == LifeColorType.Gradient)
				{
					EditorGUILayout.PropertyField(lifeGradient, new GUIContent("Gradient"));
				}
			}

			EditorTools.EndGroupBox();
		}

		void DrawRenderProperty(SerializedProperty properties)
		{
			SerializedProperty sortingLayer = properties.FindPropertyRelative("sortingLayer");
			SerializedProperty sortingOrder = properties.FindPropertyRelative("sortingOrder");

			EditorTools.BeginGroupBox("Render");

			sortingLayer.intValue = EditorTools.DrawSortingLayer("SortingLayer", sortingLayer.intValue);
			sortingOrder.intValue = EditorGUILayout.IntField("Order in Layer", sortingOrder.intValue);
			
			EditorTools.EndGroupBox();
		}

		void DrawMaterialProperty(SerializedProperty properties)
		{
			SerializedProperty overrideRenderer = properties.FindPropertyRelative("overrideRenderer");

			if(EditorTools.BeginToggleGroupBox("Override Material", overrideRenderer))
			{
				SerializedProperty material = properties.FindPropertyRelative("material");

				material.objectReferenceValue = EditorGUILayout.ObjectField("Material", material.objectReferenceValue, typeof(Material), false);
			}
			EditorTools.EndGroupBox();
		}

		void DrawScaleProperty(SerializedProperty properties)
		{
			SerializedProperty overrideScale = properties.FindPropertyRelative("overrideScale");

			if(EditorTools.BeginToggleGroupBox("Scale Multiplier", overrideScale))
			{
				SerializedProperty scaleType = properties.FindPropertyRelative("scaleType");

				ScaleType scaleTypeEnum = (ScaleType)EditorGUILayout.EnumPopup("Scale Type", (ScaleType)scaleType.intValue);
				scaleType.intValue = (int)scaleTypeEnum;

				if(scaleTypeEnum == ScaleType.Static)
				{
					SerializedProperty scale = properties.FindPropertyRelative("scale");
					scale.vector2Value = EditorGUILayout.Vector2Field("Vector2", scale.vector2Value);
				}
				else if(scaleTypeEnum == ScaleType.Curve)
				{
					SerializedProperty scaleCurveX = properties.FindPropertyRelative("scaleCurveX");
					SerializedProperty scaleCurveY = properties.FindPropertyRelative("scaleCurveY");

					scaleCurveX.animationCurveValue = EditorGUILayout.CurveField("Scale on X", scaleCurveX.animationCurveValue);
					scaleCurveY.animationCurveValue = EditorGUILayout.CurveField("Scale on Y", scaleCurveY.animationCurveValue);
				}
			}

			EditorTools.EndGroupBox();
		}
	}
}