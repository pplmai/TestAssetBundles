using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneBuilder : MonoBehaviour
{
	void Start()
	{
		Caching.CleanCache();

		StartCoroutine(LoaderQueue());
	}
	IEnumerator LoaderQueue()
	{
		yield return StartCoroutine(AssetBundlesLoader.inst().LoadAsset("http://maimai.comlu.com/AssetBundles/background",1,"background"));
		Debug.Log ("background completed");
		yield return StartCoroutine(AssetBundlesLoader.inst().LoadAsset("http://maimai.comlu.com/AssetBundles/prefab",1,"prefab"));
		Debug.Log ("prefab completed");
		yield return null;

		DoSomething();
	}
	void DoSomething()
	{
		GameObject container = Instantiate(AssetBundlesLoader.dict["prefab"].LoadAsset("Container")) as GameObject;
		container.GetComponent<SpriteRenderer>().sprite = AssetBundlesLoader.dict["background"].LoadAsset<Sprite>("bg");
	}
}
