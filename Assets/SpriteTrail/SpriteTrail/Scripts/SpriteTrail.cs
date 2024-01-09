using UnityEngine;
using UnityEngine.Rendering;

namespace Trail2D
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteTrail: MonoBehaviour
	{
		[SerializeField]
		private TrailPropertyAsset m_PropertyAsset = null;
		public TrailPropertyAsset propertyAsset {
			get { return m_PropertyAsset; }
		}

		[SerializeField]
		private TrailProperties m_Properties = new TrailProperties();
		public TrailProperties properties {
			get { return m_Properties; }
		}

		[SerializeField]
		private bool m_PlayOnAwake = true;
		[SerializeField]
		private bool m_RemoveTrailOnDisabled = false;
		public bool removeTrailOnDisabled {
			get { return m_RemoveTrailOnDisabled; }
			set { m_RemoveTrailOnDisabled = value; }
		}

		private bool m_IsPlaying = false;
		public bool isPlaying {
			get { return m_IsPlaying; }
			set { m_IsPlaying = value; }
		}

		private Transform m_Transform = null;
		private Vector3 m_LastPosition;
		private Quaternion m_LastRotation;

		private float m_MovingTime = 0f;
		private float m_MovingDistance = 0f;

		private SpriteRenderer m_Source = null;

		//private CircularArray<TrailElement> m_ActiveElements = new CircularArray<TrailElement>(128);

		private TrailProperties m_CurrentProperties;
		private TrailProperties currentProperties {
			get { return m_CurrentProperties; }
		}

		void Awake()
		{
			m_Transform = transform;
			m_Source = GetComponent<SpriteRenderer>();
		}

		void OnEnable()
		{
			if(m_PlayOnAwake)
			{
				m_IsPlaying = true;
			}

			m_LastPosition = transform.position;
			m_LastRotation = transform.rotation;
			m_Transform.hasChanged = false;
		}

		void OnDisable()
		{
			if(m_RemoveTrailOnDisabled)
			{
				RemoveAll();
			}
		}

		void LateUpdate()
		{
			m_CurrentProperties = m_PropertyAsset != null ? m_PropertyAsset.properties : m_Properties;

			if(m_IsPlaying)
			{
				UpdateEmit();
			}
			else
			{
				m_MovingTime = 0f;
				m_MovingDistance = 0f;
			}

			//UpdateElementsOrder();

			m_LastPosition = m_Transform.position;
			m_LastRotation = m_Transform.rotation;
		}

		void UpdateEmit()
		{
			float deltaTime = Time.deltaTime;
			if(m_Transform.hasChanged ||
				(m_CurrentProperties.emitMethod == EmitMethod.Time && !m_CurrentProperties.onlyWhenMoved))
			{
				if(m_CurrentProperties.emitMethod == EmitMethod.Time)
				{
					EmitByTime(deltaTime);
				}
				else
				{
					EmitByDistance(deltaTime);
				}

				if(m_Transform.hasChanged)
					m_Transform.hasChanged = false;
			}
		}

		/*
		void UpdateElementsOrder()
		{
			float deltaTime = Time.deltaTime;

			int sortingOrder = m_Source.sortingOrder;
			for(int i = 0; i < m_ActiveElements.length; i++)
			{
				TrailElement element = m_ActiveElements[i];
				if(element != null)
				{
					element.Update(deltaTime);
					element.spriteRenderer.sortingOrder = sortingOrder - m_ActiveElements.length;
					sortingOrder++;

					if(element.IsComplete())
					{
						Remove(element);
						m_ActiveElements[i] = null;
					}
				}
			}
			m_ActiveElements.TrimStart();
		}
		*/

		void Remove(TrailElement element)
		{
			if(element == null)
				return;

			PoolManager.instance.spritePool.Release(element.spriteRenderer);
			PoolManager.instance.elementPool.Release(element);
		}

		bool GetFactor(float delay, float lastValue, float movingValue, out float factor)
		{
			factor = 0;
			if(lastValue + movingValue > delay)
			{
				float needDelay = delay - lastValue;
				factor = Mathf.Clamp01(needDelay / movingValue);

				return true;
			}
			return false;
		}

		void EmitByTime(float deltaTime)
		{
			Vector3 lastPosition = m_LastPosition;
			Quaternion lastRotation = m_LastRotation;

			float factor;
			while(GetFactor(m_CurrentProperties.emitDelay, m_MovingTime, deltaTime, out factor))
			{
				float emitTime = deltaTime * (1f - factor);

				Vector3 emitPosition = Vector3.Lerp(lastPosition, m_Transform.position, factor);
				Quaternion emitRotation = Quaternion.Lerp(lastRotation, m_Transform.rotation, factor);

				Emit(emitPosition, emitRotation, emitTime, m_CurrentProperties);

				m_MovingTime = 0f;
				deltaTime = deltaTime * (1f - factor);

				lastPosition = emitPosition;
				lastRotation = emitRotation;
			}

			m_MovingTime += deltaTime;
		}

		void EmitByDistance(float deltaTime)
		{
			Vector3 lastPosition = m_LastPosition;
			Quaternion lastRotation = m_LastRotation;
			float movingDistance = Vector3.Distance(m_LastPosition, m_Transform.position);

			float factor;
			while(GetFactor(m_CurrentProperties.emitDistance, m_MovingDistance, movingDistance, out factor))
			{
				float emitTime = deltaTime * (1f - factor);

				Vector3 emitPosition = Vector3.Lerp(lastPosition, m_Transform.position, factor);
				Quaternion emitRotation = Quaternion.Lerp(lastRotation, m_Transform.rotation, factor);

				Emit(emitPosition, emitRotation, emitTime, m_CurrentProperties);

				m_MovingDistance = 0f;
				movingDistance = movingDistance * (1f - factor);

				lastPosition = emitPosition;
				lastRotation = emitRotation;
			}

			m_MovingDistance += movingDistance;
		}

		void Emit(Vector2 position, Quaternion rotation, float time, TrailProperties properties)
		{
			TrailElement element = PoolManager.instance.GetElement();

			SpriteRenderer spriteRenderer = PoolManager.instance.GetSpriteRenderer();
			spriteRenderer.gameObject.SetActive(true);

			SortingGroup group = PoolManager.instance.GetSortingGroup(this);
			if(group.gameObject.activeInHierarchy == false)
			{
				group.gameObject.SetActive(true);
				group.transform.position = Vector3.zero;
				group.transform.rotation = Quaternion.identity;
				group.transform.localScale = Vector3.one;
			}

			group.sortingLayerID = properties.sortingLayer;
			group.sortingOrder = properties.sortingOrder;

			spriteRenderer.transform.parent = group.transform;

			element.Init(this, group, m_Source, spriteRenderer, properties);
			spriteRenderer.transform.position = position;
			spriteRenderer.transform.rotation = rotation;

			element.Update(time, 0);

			SpriteTrailManager.instance.AddElement(this, element);
		}

		public void Emit()
		{
			TrailProperties properties = m_PropertyAsset != null ? m_PropertyAsset.properties : m_Properties;
			Emit(m_Transform.position, m_Transform.rotation, 0f, properties);
		}

		public void Emit(TrailProperties properties)
		{
			if(properties != null)
				Emit(m_Transform.position, m_Transform.rotation, 0f, properties);
		}

		public void Emit(TrailPropertyAsset asset)
		{
			if(asset != null && asset.properties != null)
				Emit(m_Transform.position, m_Transform.rotation, 0f, asset.properties);
		}

		public void Emit(Vector2 position, Quaternion rotation, TrailProperties properties)
		{
			Emit(position, rotation, 0f, properties);
		}

		public void RemoveAll()
		{
			SpriteTrailManager.instance.RemoveAll(this);
		}

		public void Play()
		{
			m_IsPlaying = true;
		}

		public void Stop()
		{
			m_IsPlaying = false;
			m_MovingDistance = 0f;
			m_MovingTime = 0f;
		}
	}
}