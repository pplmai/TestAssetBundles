using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ABM_Canvas : MonoBehaviour
{
	[SerializeField] Text titleText;
	[SerializeField] Transform body;
	[SerializeField] Color loadColor,unloadColor;

	Dictionary<string,BundleSlot> slots;

	public void SetTitle(string title)
	{
		titleText.text = title;
	}

	public void AddSlot(string name,string hash)
	{
		if(slots == null) slots = new Dictionary<string,BundleSlot>();
		BundleSlot slot = CreateBundleSlot(name,hash);
		slot.SetProgress(0);
		slot.transform.localPosition = new Vector3(0,-30*body.childCount,0);
		slot.transform.SetParent(body,false);
		slots.Add(name,slot);
	}

	public void SetProgress(string name,float progress)
	{
		if(slots == null) slots = new Dictionary<string,BundleSlot>();
		if(slots.ContainsKey(name))
			slots[name].SetProgress(progress);
	}

	public void SetColorTheme(string name,bool isLoad)
	{
		if(slots == null) slots = new Dictionary<string,BundleSlot>();
		if(slots.ContainsKey(name))
			slots[name].SetColor(isLoad?loadColor:unloadColor);
	}

	private static BundleSlot CreateBundleSlot(string name,string hash)
	{
		GameObject obj = Instantiate(Resources.Load("AssetBundlesLoader/BundleSlot",typeof(GameObject))) as GameObject;
		BundleSlot slot = obj.GetComponent<BundleSlot>();
		slot.SetTitle(name,hash);
		return slot;
	}
}
