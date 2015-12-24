using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{	
	Vector3 up,down,left,right;

	void Start()
	{
		up = new Vector3(0,1,0);
		down = new Vector3(0,-1,0);
		left = new Vector3(-1,0,0);
		right = new Vector3(1,0,0);
	}
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			transform.Translate(up);
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			transform.Translate(left);
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			transform.Translate(down);
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			transform.Translate(right);
		}
	}
}
