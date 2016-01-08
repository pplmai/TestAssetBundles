﻿using UnityEngine;
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

	[SerializeField] SpriteRenderer bar;

	public void UpdateProgressBar ()
	{
		bar.transform.localScale = new Vector3(_progress*10,1,1);
	}
}