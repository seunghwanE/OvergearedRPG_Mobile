using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Trail2D
{
	public class TrailElement
	{
		private SpriteTrail m_Parent = null;
		public SpriteTrail parent {
			get { return m_Parent; }
		}

		private SortingGroup m_Group = null;
		public SortingGroup group {
			get { return m_Group; }
		}

		private SpriteRenderer m_SpriteRenderer = null;
		public SpriteRenderer spriteRenderer {
			get { return m_SpriteRenderer; }
		}

		private Transform m_ElementTransform = null;

		private TrailProperties m_Properties;

		private float m_Time = 0;
		private float m_Factor = 0;

		private int m_StartSortingOrder;
		//private Color m_StartColor;
		private Vector3 m_StartScale;

		public void Init(SpriteTrail parent, SortingGroup group, SpriteRenderer source, SpriteRenderer target, TrailProperties properties)
		{
			m_Time = 0f;
			m_Parent = parent;
			m_Group = group;

			m_SpriteRenderer = target;
			m_ElementTransform = target.transform;

			m_StartSortingOrder = source.sortingOrder;
			m_StartScale = source.transform.lossyScale;
			//m_StartColor = source.color;

			m_Properties = properties;

			ApplyInitialProperties(source);
		}

		void ApplyInitialProperties(SpriteRenderer source)
		{
			Transform sourceTrans = source.transform;
			Transform targetTrans = spriteRenderer.transform;

			targetTrans.localScale = sourceTrans.lossyScale;

			m_SpriteRenderer.sortingLayerID = m_Properties.sortingLayer;

			m_SpriteRenderer.sprite = source.sprite;
			m_SpriteRenderer.color = source.color;

			if(m_Properties.overrideRenderer && m_Properties.material != null)
				m_SpriteRenderer.material = m_Properties.material;
		}

		public void Update(float deltaTime, int sequence)
		{
			m_Time += deltaTime;
			m_Factor = m_Time / m_Properties.lifeTime;

			if(m_Properties.overrideColor)
				UpdateColor();

			if(m_Properties.overrideScale)
				UpdateScale();

			m_SpriteRenderer.sortingOrder = m_StartSortingOrder - sequence - 1;
		}

		void UpdateColor()
		{
			if(m_Properties.lifeColorType == LifeColorType.Solid)
				UpdateSolidColor();
			else
				UpdateGradientColor();
		}

		void UpdateScale()
		{
			if(m_Properties.scaleType == ScaleType.Static)
			{
				m_ElementTransform.localScale = new Vector3(
					m_StartScale.x * m_Properties.scale.x,
					m_StartScale.y * m_Properties.scale.y,
					m_StartScale.z);
			}
			else if(m_Properties.scaleType == ScaleType.Curve)
			{
				m_ElementTransform.localScale = new Vector3(
					m_StartScale.x * m_Properties.scaleCurveX.Evaluate(m_Factor),
					m_StartScale.y * m_Properties.scaleCurveY.Evaluate(m_Factor),
					m_StartScale.z);
			}
		}

		void UpdateSolidColor()
		{
			Color color = m_Properties.lifeColor;
			color.a = m_Properties.lifeColor.a * m_Properties.alphaCurve.Evaluate(m_Factor);

			m_SpriteRenderer.color = color;
		}

		void UpdateGradientColor()
		{
			Color color = m_Properties.lifeGradient.Evaluate(m_Factor);

			m_SpriteRenderer.color = color;
		}

		public bool IsComplete()
		{
			if(m_Properties.lifeTime <= m_Time)
				return true;

			return false;
		}
	}
}