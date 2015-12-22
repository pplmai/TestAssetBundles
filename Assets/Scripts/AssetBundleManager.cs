using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour
{
	// A dictionary to hold the AssetBundle references
	private static Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
	private static AssetBundleManager _inst;
	public static AssetBundleManager inst
	{
		get
		{
			if(_inst == null)
			{
				_inst = new AssetBundleManager();
			}
			return _inst;
		}
	}
	void Start()
	{
		_inst = this;
		DontDestroyOnLoad(this);
		dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
	}
	// Class with the AssetBundle reference, url and version
	private class AssetBundleRef
	{
		public AssetBundle assetBundle = null;
		public int version;
		public string keyName;
		public AssetBundleRef(string strKeyIn, int intVersionIn)
		{
			keyName = strKeyIn;
			version = intVersionIn;
		}
	};
	// Get an AssetBundle
	public static AssetBundle getAssetBundle (string keyName, int version)
	{
		keyName = keyName + version.ToString();
		AssetBundleRef abRef;
		if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
			return abRef.assetBundle;
		else
			return null;
	}
	// Download an AssetBundle
	public static IEnumerator downloadAssetBundle (string url, int version)
	{
		string bundleName = GetBundleName(url);
		string keyName = bundleName + version.ToString();
		if (dictAssetBundleRefs.ContainsKey(keyName))
			yield return null;
		else
		{
			using(WWW www = WWW.LoadFromCacheOrDownload (url, version))
			{
				LoaderBar bar = CreateLoaderBar();
				while(!www.isDone)
				{
					bar.progress = www.progress;
					yield return null;
				}
				Destroy(bar.gameObject);
				if (www.error != null)
					throw new Exception("WWW download:" + www.error);
				AssetBundleRef abRef = new AssetBundleRef (url, version);
				abRef.assetBundle = www.assetBundle;
				dictAssetBundleRefs.Add (keyName, abRef);
				Debug.Log ("<color=green>AssetBundleManager: </color><color=yellow>"+bundleName+" v"+version+"</color><color=green> load completed</color>");
			}
		}
	}
	// Unload an AssetBundle
	public static void Unload (string keyName, int version, bool allObjects)
	{
		keyName = keyName + version.ToString();
		AssetBundleRef abRef;
		if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
		{
			abRef.assetBundle.Unload (allObjects);
			abRef.assetBundle = null;
			dictAssetBundleRefs.Remove(keyName);
		}
	}
#region Helper
	private static LoaderBar CreateLoaderBar()
	{
		GameObject bar = Instantiate(Resources.Load("AssetBundlesLoader/LoaderBar",typeof(GameObject))) as GameObject;
		return bar.GetComponent<LoaderBar>();
	}
	private static string GetBundleName(string url)
	{
		string[] strs = url.Split('/');
		return strs[strs.Length-1];
	}
#endregion
}

