using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour {

	private FacetrackingManager manager;
	private KinectInterop.DepthSensorPlatform platform;

	public SkinnedMeshRenderer eyelidLeft;
	public SkinnedMeshRenderer eyelidRight;

	public float cooldown;
	private float timeOfLastChangeLeft;
	private float timeOfLastChangeRight;

	[Range(-1.0f, 1.0f)]
	public float blinkLimit;

	// Use this for initialization
	void Start () {
		KinectManager kinectManager = KinectManager.Instance;
		if (kinectManager && kinectManager.IsInitialized())
		{
			platform = kinectManager.GetSensorPlatform();
		}
	}

	// Update is called once per frame
	void Update()
	{   
		// get the face-tracking manager instance
		if (manager == null)
		{
			manager = FacetrackingManager.Instance;
		}
		if (manager && manager.GetFaceTrackingID() != 0)
		{
			// AU6, AU7 – eyelid closed
			// 0=neutral; -1=raised; +1=fully lowered
			float fAU6_left = manager.GetAnimUnit(KinectInterop.FaceShapeAnimations.LefteyeClosed);
			fAU6_left = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU6_left * 2 - 1) : fAU6_left;
			float fAU6_right = manager.GetAnimUnit(KinectInterop.FaceShapeAnimations.RighteyeClosed);
			fAU6_right = (platform == KinectInterop.DepthSensorPlatform.KinectSDKv2) ? (fAU6_right * 2 - 1) : fAU6_right;
			if (Time.time - timeOfLastChangeLeft > cooldown)
			{
				if (fAU6_left > blinkLimit && fAU6_right > blinkLimit)
				{
					eyelidLeft.enabled = true;
					eyelidRight.enabled = true;
					timeOfLastChangeLeft = Time.time;
				}
				else
				{
					eyelidLeft.enabled = false;
					eyelidRight.enabled = false;
					timeOfLastChangeLeft = Time.time;
				}
			}
		}
	}
}
