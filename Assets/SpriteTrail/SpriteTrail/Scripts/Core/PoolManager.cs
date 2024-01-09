using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Trail2D
{
	public class PoolManager: MonoBehaviour
	{
		public readonly UnityObjectPool<SpriteRenderer> spritePool = new UnityObjectPool<SpriteRenderer>("SpriteTrail");
		public readonly UnityObjectPool<SortingGroup> groupPool = new UnityObjectPool<SortingGroup>("TrailGroup");
		public readonly ObjectPool<TrailElement> elementPool = new ObjectPool<TrailElement>();

		public readonly Dictionary<SpriteTrail, SortingGroup> activatedGroups = new Dictionary<SpriteTrail, SortingGroup>();

		private static PoolManager _instance = null;
		public static PoolManager instance {
			get {
				if(_instance == null)
				{
					GameObject go = new GameObject("SpriteTrail Pool", typeof(PoolManager));
					_instance = go.GetComponent<PoolManager>();
				}
				return _instance;
			}
		}

		private static TrailSettings _settings = null;

		static PoolManager()
		{
			_settings = Resources.Load<TrailSettings>(TrailSettings.kAssetName);
			if(_settings == null)
				_settings = new TrailSettings();
		}

		void Start()
		{
			spritePool.Prepare(_settings.defaultPoolCount, _settings.hideInHierachy);
		}

		public TrailElement GetElement()
		{
			return elementPool.Get();
		}

		public SpriteRenderer GetSpriteRenderer()
		{
			return spritePool.Get(_settings.hideInHierachy);
		}

		public void ReleaseGroup(SortingGroup group)
		{
			if(activatedGroups.ContainsValue(group))
			{
				foreach(var keyValue in activatedGroups)
				{
					if(keyValue.Value == group)
					{
						activatedGroups.Remove(keyValue.Key);
						return;
					}
				}
			}

			groupPool.Release(group);
		}

		public SortingGroup GetSortingGroup(SpriteTrail trail)
		{
			if(activatedGroups.ContainsKey(trail))
			{
				return activatedGroups[trail];
			}
			else
			{
				SortingGroup group = groupPool.Get(_settings.hideInHierachy);
				activatedGroups[trail] = group;
				return group;
			}

		}
	}
}