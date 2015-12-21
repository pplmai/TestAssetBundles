using UnityEditor;
using UnityEngine;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("AssetBundlesOutput");
	}
}

public class GetAssetBundleNames
{
	[MenuItem ("Assets/Get AssetBundle names")]
	static void GetNames ()
	{
		var names = AssetDatabase.GetAllAssetBundleNames();
		foreach (var name in names)
			Debug.Log ("AssetBundle: " + name);
	}
}

public class MyPostprocessor : AssetPostprocessor {
	
	void OnPostprocessAssetbundleNameChanged ( string path,
	                                          string previous, string next) {
		Debug.LogWarning("AB: " + path + " old: " + previous + " new: " + next);
	}
}