using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundlesLoader : MonoBehaviour
{
	private static AssetBundlesLoader _inst = null;
	public static Dictionary<string,AssetBundle> dict;
	
	public static AssetBundlesLoader inst()
	{
		if(_inst == null)
		{
			_inst = new AssetBundlesLoader();
		}
		return _inst;
	}

	void Start()
	{
		_inst = this;
		dict = new Dictionary<string, AssetBundle>();
	}

	public IEnumerator LoadAsset(string path,int version,string key)
	{
		yield return StartCoroutine(LoadAssetCoroutine (path,version,key));
	}

	private IEnumerator LoadAssetCoroutine(string path,int version,string key)
	{
		while (!Caching.ready)
			yield return null;
		
		LoaderBar bar = CreateLoaderBar();

		using(WWW www = WWW.LoadFromCacheOrDownload(path,version))
		{
			while(!www.isDone)
			{
				bar.progress = www.progress;
				yield return null;
			}
			Destroy(bar.gameObject);

			if(www.error != null)
			{
				throw new UnityException("WWW download had an error: "+www.error);
			}
			dict.Add(key,www.assetBundle);
		}
		yield return null;
	}

#region Helper
	private LoaderBar CreateLoaderBar()
	{
		GameObject bar = Instantiate(Resources.Load("AssetBundlesLoader/LoaderBar",typeof(GameObject))) as GameObject;
		return bar.GetComponent<LoaderBar>();
	}
#endregion
}
