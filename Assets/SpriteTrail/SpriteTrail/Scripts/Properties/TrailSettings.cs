using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class TrailSettings : ScriptableObject
{
	public const string kAssetName = "trail_settings";
	public const string kPath = "Assets/SpriteTrail/Resources/" + kAssetName + ".asset";

	public bool dontDestroyPool = true;
	public bool hideInHierachy = true;
	public int defaultPoolCount = 64;
}
