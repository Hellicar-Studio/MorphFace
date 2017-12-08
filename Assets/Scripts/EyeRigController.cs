using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRigController : MonoBehaviour {
	private FacetrackingManager manager;
	public int PlayerIndex = 0;
	public RotationChecker rotationChecker;
	public BlinkController blinkController;
	public ModelFaceController modelFaceController;
	// Use this for initialization
	void Start () {
		if(modelFaceController == null)
		{
			modelFaceController = GetComponent<ModelFaceController>();
		}
		if (blinkController == null)
		{
			blinkController = GetComponent<BlinkController>();
		}
		if(rotationChecker == null)
		{
			rotationChecker = GetComponent<RotationChecker>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(manager == null)
		{
			manager = FacetrackingManager.Instance;
		}
		manager.playerIndex = PlayerIndex;
		manager.ManageFacetracking();
		modelFaceController.ControlModelFace();
		rotationChecker.CheckRotation();
		blinkController.ControlBlinks();
	}
}
