using UnityEngine;
using System.Collections;
using AssetBundles;


public class Experiment : MonoBehaviour
{
	IEnumerator Start()
	{
//		AssetBundleManager.SetDevelopmentAssetBundleServer();
		AssetBundleManager.SetSourceAssetBundleURL("http://maimai.comlu.com/AssetBundles/");

		var request = AssetBundleManager.Initialize();
		if(request != null)
		{
			yield return StartCoroutine(request);
		}

		AssetBundleManifest manifest = AssetBundleManager.AssetBundleManifestObject;
		string[] names = manifest.GetAllAssetBundles();
		string[] names2 = manifest.GetAllAssetBundlesWithVariant();

		string[] vari = {""};
		AssetBundleManager.ActiveVariants = vari;

		AssetBundleLoadAssetOperation eiei = AssetBundleManager.LoadAssetAsync("mapbg","BG_Map1",typeof(GameObject));
		yield return StartCoroutine(eiei);

		GameObject prefab = eiei.GetAsset<GameObject>();
		GameObject clone = Instantiate(prefab);
	}
}
