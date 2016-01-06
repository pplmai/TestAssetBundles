using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CarperMaterialData : MonoBehaviour
{
	Material mat;
	[SerializeField][Range(0f,1f)] float time = 0;
	[SerializeField][Range(0f,30f)]float frequency = 0;
	[SerializeField][Range(0f,20f)]float strength = 0;
	[SerializeField][Range(-1f,1f)]float pivot = 0;
	[SerializeField] bool preview;
	[SerializeField]Color color;

	void Start ()
	{
		if(Application.isPlaying)
			mat = GetComponent<MeshRenderer>().material;
		else
			mat = GetComponent<MeshRenderer>().sharedMaterial;
	}

	void Update()
	{
		mat.SetFloat("_T",time);
		mat.SetFloat("_Frequency",frequency);
		mat.SetFloat("_Strength",strength);
		mat.SetColor("_Color",color);
		mat.SetFloat("_Pivot",pivot);
		mat.SetFloat ("_UseTime",(preview?1f:0f));
	}
}
