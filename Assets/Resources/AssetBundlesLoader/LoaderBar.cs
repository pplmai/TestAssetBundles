using UnityEngine;
using System.Collections;

public class LoaderBar : MonoBehaviour
{
	[HideInInspector] public float progress
	{
		get
		{
			return _progress;
		}
		set
		{
			_progress = value;
			UpdateProgressBar();

		}
	}
	private float _progress;

	[HideInInspector] public string key
	{
		get
		{
			return _key;
		}
		set
		{
			_key = value;
			text.text = _key;
		}
	}
	private string _key;

	[SerializeField] SpriteRenderer bar;
	[SerializeField] TextMesh text;

	public void UpdateProgressBar ()
	{
		bar.transform.localScale = new Vector3(_progress*10,1,1);
	}
}
