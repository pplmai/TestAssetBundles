using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetBundlesLoaderScene : MonoBehaviour
{
	void Start()
	{
		Caching.CleanCache();
		StartCoroutine(LoaderQueue());
	}
	IEnumerator LoaderQueue()
	{
		yield return StartCoroutine(AssetBundlesManager.inst().LoadAsset("http://maimai.comlu.com/AssetBundles/prefab",1,"prefab"));
		Debug.Log ("prefab completed");

		yield return StartCoroutine(AssetBundlesManager.inst().LoadAsset("http://maimai.comlu.com/AssetBundles/background.xmas",1,"background"));
		Debug.Log ("background completed");

		DoSomething();

		yield return null;
	}
	void DoSomething()
	{
		AssetBundle bundle = AssetBundlesManager.dict["prefab"];
		GameObject container = Instantiate(bundle.LoadAsset("Container")) as GameObject;
		bundle.Unload(false);
       	bundle = AssetBundlesManager.dict["prefab"];
		container = Instantiate(bundle.LoadAsset("Container")) as GameObject;
//		container.GetComponent<SpriteRenderer>().sprite = AssetBundlesManager.dict["background"].LoadAsset<Sprite>("bg");
	}
}
