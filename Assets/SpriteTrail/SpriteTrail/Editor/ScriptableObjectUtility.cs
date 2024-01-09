using UnityEngine;
using UnityEditor;
using System.IO;

namespace Trail2D
{
	public static class ScriptableObjectUtility
	{
		public static T CreateAssetFilePanel<T>(string defaultFileName) where T : ScriptableObject
		{
			string path = EditorUtility.SaveFilePanel("Export Properties", "Assets", defaultFileName, "asset");
			return CreateAsset<T>(path);
		}

		/// <summary>
		//	This makes it easy to create, name and place unique new ScriptableObject asset files.
		/// </summary>
		public static T CreateAsset<T>(string path) where T : ScriptableObject
		{
			if(!string.IsNullOrEmpty(path))
			{
				path = path.Replace(Application.dataPath, "Assets");

				T asset = ScriptableObject.CreateInstance<T>();
				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();
				//EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
				return asset;
			}
			return null;
		}
	}
}