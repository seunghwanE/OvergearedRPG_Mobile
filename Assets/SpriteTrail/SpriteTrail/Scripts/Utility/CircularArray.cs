using UnityEngine;
using UnityEngine.Assertions;

namespace Trail2D
{
	public class CircularArray<T> where T : new()
	{
		private T[] m_Array = null;
		public T[] array {
			get { return m_Array; }
			private set { m_Array = value; }
		}

		public T this[int index] {
			get {
				int i = (m_StartIndex + index) % capacity;
				return m_Array[i];
			}
			set { Insert(value, index); }
		}

		public int capacity {
			get { return m_Array != null ? m_Array.Length : 0; }
		}

		private int m_StartIndex = 0;

		private int m_Length = 0;
		public int length {
			get { return m_Length; }
			private set { m_Length = value; }
		}

		public CircularArray(int defaultCapacity)
		{
			Assert.IsTrue(defaultCapacity > 0, "Length must be bigger than zero.");

			m_Array = new T[defaultCapacity];
		}

		public void Clear()
		{
			m_StartIndex = 0;
			length = 0;
		}

		public void SetLength(int length)
		{
			this.length = length;
		}

		public void TrimStart()
		{
			int count = 0;
			int startIndex = m_StartIndex;
			for(int i = 0; i < length; i++)
			{
				int index = (m_StartIndex + i) % capacity;
				if(m_Array[index] != null)
				{
					startIndex = index;
					break;
				}

				count++;
			}
			m_Length -= count;
			m_StartIndex = startIndex;
		}

		public void Rescale(int newCapacity)
		{
			Assert.IsTrue(newCapacity > 0, "Length must be bigger than zero.");

			if(capacity != newCapacity)
			{
				T[] newArray = new T[newCapacity];

				for(int i = 0; i < length && i < newArray.Length; i++)
				{
					int index = (m_StartIndex + i) % capacity;
					newArray[i] = m_Array[index];
				}

				m_StartIndex = 0;
				m_Length = Mathf.Min(length, newArray.Length);
				m_Array = newArray;
			}
		}

		public void Add(T item)
		{
			if(length >= capacity)
				Rescale(capacity * 2);

			int index = (m_StartIndex + length) % capacity;
			m_Array[index] = item;
			length++;
		}

		public void Insert(T item, int i)
		{
			if(i >= capacity)
				Rescale(i * 2);

			int index = (m_StartIndex + i) % capacity;
			m_Array[index] = item;

			if(i >= length)
				length = i + 1;
		}

	}
}