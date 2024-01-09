using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trail2D
{
	public class UnityObjectPool<T> where T : Component
	{
		private Stack<T> m_Stack = new Stack<T>();

		public int countAll { get; private set; }
		public int countActive { get { return countAll - countInactive; } }
		public int countInactive { get { return m_Stack.Count; } }

		private string m_ObjectName = "";

		public UnityObjectPool(string name)
		{
			m_ObjectName = name;
		}

		public void Prepare(int count, bool hideInHierachy)
		{
			if(count > countAll)
			{
				for(int i = m_Stack.Count; i < count; i++)
				{
					T newObject = Create(hideInHierachy);
					m_Stack.Push(newObject);
				}
			}
		}

		public T Get(bool hideInHierachy)
		{
			T spriteRenderer = GetOrCreate(hideInHierachy);

			return spriteRenderer;
		}

		public void Release(T o)
		{
			if(o != null)
			{
				o.transform.parent = null;
				o.gameObject.SetActive(false);
				m_Stack.Push(o);
			}
		}

		public void Clear()
		{
			if(m_Stack != null)
			{
				m_Stack.Clear();
				countAll = 0;
			}
		}

		public void DestroyAll()
		{
			while(m_Stack != null && m_Stack.Count > 0)
			{
				var instance = m_Stack.Pop();
				GameObject.Destroy(instance.gameObject);
			}
			countAll = 0;
		}

		T GetOrCreate(bool hideInHierachy)
		{
			T o = Get();
			if(o == null)
				o = Create(hideInHierachy);

			return o;
		}

		T Get()
		{
			if(m_Stack.Count > 0)
			{
				T o = m_Stack.Pop();
				//o.gameObject.SetActive(true);

				return o;
			}
			return null;
		}

		T Create(bool hideInHierachy)
		{
			GameObject go = new GameObject(m_ObjectName);

			if(hideInHierachy)
				go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			countAll++;

			return go.AddComponent<T>();
		}
	}
}