using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BundleSlot : MonoBehaviour
{
	[SerializeField] Text titleText;
	[SerializeField] Image progressBg,progressBar;

	public void SetTitle(string name,string hash)
	{
		titleText.text = name;// + " : " + hash;
	}

	public void SetColor(Color color)
	{
		progressBar.color = color;
	}

	public void SetProgress(float progress)
	{
		progressBar.transform.localScale = new Vector3(progress,1,1);
	}
}
