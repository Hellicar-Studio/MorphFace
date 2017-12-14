using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

	public Material mat;

	public Slider sMin;
	public Slider sMax;

	// Use this for initialization
	void Start () {
		sMin.value = PlayerPrefs.GetFloat("_StrengthMin");
		sMax.value = PlayerPrefs.GetFloat("_StrengthMax");
	}

	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("_StrengthMin", sMin.value);
		PlayerPrefs.SetFloat("_StrengthMax", sMax.value);

		mat.SetFloat("_StrengthMin", sMin.value);
		mat.SetFloat("_StrengthMax", sMax.value);
	}
}
