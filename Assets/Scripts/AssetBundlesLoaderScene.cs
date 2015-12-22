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
		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle("http://maimai.comlu.com/AssetBundles/scene",1));
//		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle("http://maimai.comlu.com/AssetBundles/map3",1));

		DoSomething();

		yield return null;
	}
	void DoSomething()
	{
//		AssetBundle bundle = AssetBundleManager.getAssetBundle("prefab",1);
//		GameObject container = Instantiate(bundle.LoadAsset("Container")) as GameObject;
//		container = Instantiate(bundle.LoadAsset("Container")) as GameObject;
		Application.LoadLevel("LevelSelect3");
	}
}
