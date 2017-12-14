using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

	public EffectApplier effect;

	public Slider sMin;
	public Slider maxDistort;
	public Slider yOffset;

	public GameObject controls;

	// Use this for initialization
	void Start () {
		sMin.value = PlayerPrefs.GetFloat("_StrengthMin");
		maxDistort.value = PlayerPrefs.GetFloat("_MaxDistort");
		yOffset.value = PlayerPrefs.GetFloat("_YOffset");
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("_StrengthMin", sMin.value);
		PlayerPrefs.SetFloat("_MaxDistort", maxDistort.value);
		PlayerPrefs.SetFloat("_YOffset", yOffset.value);

		effect.yOffset = yOffset.value;
		effect.effect.SetFloat("_StrengthMin", sMin.value);
		effect.effect.SetFloat("_MaxDistort", maxDistort.value);

		if(Input.GetKeyUp("g"))
		{
			controls.SetActive(!controls.active);
			Cursor.visible = controls.active;
		}
	}
}
