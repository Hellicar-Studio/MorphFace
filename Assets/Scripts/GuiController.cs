using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

	public Material mat;

	public Slider sMin;
	public Slider maxDistort;

	public GameObject controls;

	// Use this for initialization
	void Start () {
		sMin.value = PlayerPrefs.GetFloat("_StrengthMin");
		Cursor.visible = false;
		maxDistort.value = PlayerPrefs.GetFloat("_MaxDistort");
	}

	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("_StrengthMin", sMin.value);
		PlayerPrefs.SetFloat("_MaxDistort", maxDistort.value);

		mat.SetFloat("_StrengthMin", sMin.value);
		mat.SetFloat("_MaxDistort", maxDistort.value);

		if(Input.GetKeyUp("g"))
		{
			controls.SetActive(!controls.active);
			Cursor.visible = controls.active;
		}
	}
}
