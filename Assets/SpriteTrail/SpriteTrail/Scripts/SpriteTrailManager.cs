using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Trail2D
{
	public class SpriteTrailManager: MonoBehaviour
	{
		private Dictionary<SpriteTrail, List<TrailElement>> m_ElementTable = new Dictionary<SpriteTrail, List<TrailElement>>();
		private List<SpriteTrail> m_KeysToRemove = new List<SpriteTrail>();

		private static SpriteTrailManager _instance = null;
		public static SpriteTrailManager instance {
			get {
				if(_instance == null)
				{
					GameObject manager = new GameObject("SpriteTrailManager", typeof(SpriteTrailManager));
					_instance = manager.GetComponent<SpriteTrailManager>();
				}
				return _instance;
			}
		}

		void LateUpdate()
		{
			UpdateElements(Time.deltaTime);
		}

		void UpdateElements(float deltaTime)
		{
			foreach(KeyValuePair<SpriteTrail, List<TrailElement>> keyVal in m_ElementTable)
			{
				List<TrailElement> elements = keyVal.Value;

				for(int i = elements.Count - 1; i >= 0; i--)
				{
					TrailElement element = elements[i];
					element.Update(deltaTime, elements.Count - i - 1);

					if(element.IsComplete())
					{
						elements.RemoveAt(i);
						ReleaseElement(element);

						SortingGroup group = element.group;
						if(group.transform.childCount <= 0)
						{
							ReleaseGroup(group);
						}
					}
				}

				if(elements.Count <= 0)
				{
					m_KeysToRemove.Add(keyVal.Key);
				}
			}

			if(m_KeysToRemove.Count > 0)
			{
				for(int i = 0; i < m_KeysToRemove.Count; i++)
				{
					m_ElementTable.Remove(m_KeysToRemove[i]);
					m_KeysToRemove.Clear();
				}
			}
		}

		public void AddElement(SpriteTrail parent, TrailElement element)
		{
			if(!m_ElementTable.ContainsKey(parent))
			{
				m_ElementTable.Add(parent, new List<TrailElement>());
			}

			m_ElementTable[parent].Add(element);
		}

		public void RemoveAll(SpriteTrail parent)
		{
			if(m_ElementTable.ContainsKey(parent))
			{
				var elements = m_ElementTable[parent];
				for(int i=0; i < elements.Count; i++)
				{
					TrailElement element = elements[i];
					ReleaseElement(element);

					SortingGroup group = element.group;
					if(group.transform.childCount <= 0)
					{
						ReleaseGroup(group);
					}
				}

				m_ElementTable.Remove(parent);
			}
		}

		public int GetElementCount(SpriteTrail parent)
		{
			if(m_ElementTable.ContainsKey(parent))
			{
				return m_ElementTable[parent].Count;
			}
			return 0;
		}

		void ReleaseElement(TrailElement element)
		{
			if(element == null)
				return;
			
			PoolManager.instance.spritePool.Release(element.spriteRenderer);
			PoolManager.instance.elementPool.Release(element);
		}

		void ReleaseGroup(SortingGroup group)
		{
			PoolManager.instance.ReleaseGroup(group);
		}
	}
}