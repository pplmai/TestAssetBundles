using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetRenderLayer : MonoBehaviour
{
	[SerializeField] string layerName;
	[SerializeField] int sortingOrder;
	void Update ()
	{
		GetComponent<Renderer>().sortingLayerName = layerName;
		GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
}
