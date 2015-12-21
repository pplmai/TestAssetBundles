using UnityEngine;
using System.Collections;

public class SceneBuilder : MonoBehaviour
{
	IEnumerator Start()
	{
		// Wait for the Caching system to be ready
		while (!Caching.ready)
			yield return null;

		using(WWW www = WWW.LoadFromCacheOrDownload("http://maimai.comlu.com/AssetBundles/background.hd",1))
		{
			yield return www;
			if(www.error != null)
			{
				throw new UnityException("WWW download had an error: "+www.error);
			}
			AssetBundle bundle = www.assetBundle;
			Debug.Log ("Asset Loaded");
			Instantiate(bundle.LoadAsset("TextPrefab"));

			bundle.Unload(false);
		}
	}
}
