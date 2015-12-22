using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundlesManager : MonoBehaviour
{
	private static AssetBundlesManager _inst = null;
	public static Dictionary<string,AssetBundle> dict;
	
	public static AssetBundlesManager inst()
	{
		if(_inst == null)
		{
			_inst = new AssetBundlesManager();
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

			www.Dispose();
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
