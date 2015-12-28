using UnityEngine;
using System.Collections;

public class AssetBundlesLoaderScene : MonoBehaviour
{
	void Start()
	{
//		Caching.CleanCache();
		StartCoroutine(LoaderQueue());
	}
	IEnumerator LoaderQueue()
	{
//		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle("http://maimai.comlu.com/AssetBundles/prefab",1));
		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle("http://maimai.comlu.com/AssetBundles/map3",1));

		DoSomething();

		yield return null;


	}
	void DoSomething()
	{
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("LevelSelect4");
		}
	}	
}
