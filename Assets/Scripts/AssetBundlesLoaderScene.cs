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
		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle("http://maimai.comlu.com/AssetBundles/map3",2));

		DoSomething();

		yield return null;


	}
	void DoSomething()
	{
		Application.LoadLevel("LevelSelect3");
	}
}
