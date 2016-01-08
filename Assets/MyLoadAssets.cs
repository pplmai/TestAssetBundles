using UnityEngine;
using System.Collections;
using AssetBundles;

public class MyLoadAssets : MonoBehaviour
{
	public const string AssetBundlesOutputPath = "/AssetBundles/";
	public string assetBundleName;
	public string assetName;

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return StartCoroutine(Initialize());
		
		yield return StartCoroutine(InstantiateGameObjectAsync (assetBundleName, assetName) );
	}

	protected IEnumerator Initialize()
	{
//		DontDestroyOnLoad(gameObject);

		#if (DEVELOPMENT_BUILD || UNITY_EDITOR)
		AssetBundleManager.SetDevelopmentAssetBundleServer ();
		#else
		AssetBundleManager.SetSourceAssetBundleURL("http://maimai.comlu.com/AssetBundles/");
		#endif

		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
		var request = AssetBundleManager.Initialize();
		if (request != null)
			yield return StartCoroutine(request);
	}

	protected IEnumerator InstantiateGameObjectAsync (string assetBundleName, string assetName)
	{
		// This is simply to get the elapsed time for this phase of AssetLoading.
		float startTime = Time.realtimeSinceStartup;
		// Load asset from assetBundle.
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject) );
		if (request == null)
			yield break;
		
		LoaderBar loader = CreateLoaderBar();
		while(!request.IsDone)
		{

		}

		// Get the asset.
		GameObject prefab = request.GetAsset<GameObject> ();

		if (prefab != null)
			GameObject.Instantiate(prefab);
		
		// Calculate and display the elapsed time.
		float elapsedTime = Time.realtimeSinceStartup - startTime;
		if(prefab == null)
		{
			Debug.Log("<color=red>" + assetName + " was not" + " loaded successfully in " + elapsedTime + " seconds</color>" );
		}
		else
		{
			Debug.Log("<color=green>" + assetName + " was" + " loaded successfully in " + elapsedTime + " seconds</color>" );
		}
	}

	private LoaderBar CreateLoaderBar()
	{
		GameObject bar = Instantiate(Resources.Load("AssetBundlesLoader/LoaderBar",typeof(GameObject))) as GameObject;
		return bar.GetComponent<LoaderBar>();
	}

}
