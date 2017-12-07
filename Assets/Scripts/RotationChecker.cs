using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChecker : MonoBehaviour {

	private FacetrackingManager manager;
	private KinectInterop.DepthSensorPlatform platform;

	public float angle;

	public List<GameObject> renderers;

	//[Tooltip("Transform of the joint, used to move and rotate the head.")]
	//public Transform HeadTransform;

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
			// head position
			// Vector3 newPosition = manager.GetHeadPosition(mirroredHeadMovement);

			// head rotation
			Quaternion newRotation = manager.GetHeadRotation(true);
			Debug.Log(newRotation.eulerAngles.y);
			if (newRotation.eulerAngles.y > angle && newRotation.eulerAngles.y < 360 - angle)
			{
				for (int i = 0; i < renderers.Count; i++)
				{
					renderers[i].SetActive(false);
				}
			}
			else
			{
				for (int i = 0; i < renderers.Count; i++)
				{
					renderers[i].SetActive(true);
				}
			}
		}
	}
}
