using UnityEngine;

namespace Trail2D
{
	public class SortingLayerAttribute: PropertyAttribute
	{
		public readonly string labelName = null;
		public SortingLayerAttribute(string label = null)
		{
			labelName = label;
		}
	}
}