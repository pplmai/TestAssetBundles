﻿using UnityEngine;
using System.Collections;
using AssetBundles;


public class LoadAssets : MonoBehaviour
{
	public const string AssetBundlesOutputPath = "/AssetBundles/";
	public string assetBundleName;
	public string assetName;

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return StartCoroutine(Initialize() );
		
		// Load asset.
		yield return StartCoroutine(InstantiateGameObjectAsync (assetBundleName, assetName) );
	}

	// Initialize the downloading url and AssetBundleManifest object.
	protected IEnumerator Initialize()
	{
		// Don't destroy this gameObject as we depend on it to run the loading script.
//		DontDestroyOnLoad(gameObject);

		// With this code, when in-editor or using a development builds: Always use the AssetBundle Server
		// (This is very dependent on the production workflow of the project. 
		// 	Another approach would be to make this configurable in the standalone player.)
		#if (DEVELOPMENT_BUILD || UNITY_EDITOR)
		AssetBundleManager.SetDevelopmentAssetBundleServer ();
		#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
//		AssetBundleManager.SetSourceAssetBundleDirectory("/Assets/StreamingAssets/Android/");
		// Or customize the URL based on your deployment or configuration
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
		yield return StartCoroutine(request);

		// Get the asset.
		GameObject prefab = request.GetAsset<GameObject> ();

		if (prefab != null)
		{
			GameObject clone = GameObject.Instantiate(prefab);
			clone.transform.SetParent(this.transform,false);
		}

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
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			AssetBundleManager.UnloadAssetBundle("cube-bundle");
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			AssetBundleManager.LoadAssetBundle("cube-bundle");
		}
	}
}
