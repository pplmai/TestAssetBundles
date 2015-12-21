using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AdjustCamera : MonoBehaviour
{
	[SerializeField] int pixelsPerUnit;
	Camera cam;

	void Start ()
	{
		cam = GetComponent<Camera>();
	}
	
	void Update ()
	{
		cam.orthographicSize = Screen.height/2f/pixelsPerUnit;
	}
}
