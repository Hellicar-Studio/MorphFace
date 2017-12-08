using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChecker : MonoBehaviour {

	private FacetrackingManager manager;
	private KinectInterop.DepthSensorPlatform platform;

	public float angleY;
	public float angleX;
	public float angleZ;

	public List<Material> mats;
	private List<Color> originalColors;

	private float alpha;

	//[Tooltip("Transform of the joint, used to move and rotate the head.")]
	//public Transform HeadTransform;

	// Use this for initialization
	void Start () {
		KinectManager kinectManager = KinectManager.Instance;
		if (kinectManager && kinectManager.IsInitialized())
		{
			platform = kinectManager.GetSensorPlatform();
		}
		originalColors = new List<Color>();

	}

	// Update is called once per frame
	public void CheckRotation()
	{
		if(originalColors.Count == 0)
		{
			for (int i = 0; i < mats.Count; i++)
			{
				Color col = mats[i].GetColor("_Color");
				originalColors.Add(col);
			}
		}

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
			bool lerpDown = false;
			bool outY = (newRotation.eulerAngles.y > angleY && newRotation.eulerAngles.y < 360 - angleY);
			bool outX = (newRotation.eulerAngles.x > angleX && newRotation.eulerAngles.x < 360 - angleX);
			bool outZ = (newRotation.eulerAngles.z > angleZ && newRotation.eulerAngles.z < 360 - angleZ);
			if (outY || outX || outZ)
			{
				lerpDown = true;
			}

			if(lerpDown)
			{
				alpha = Mathf.Lerp(alpha, 0, 0.05f);
			}
			else
			{
				alpha = Mathf.Lerp(alpha, 1, 0.05f);
			}

			for (int i = 0; i < mats.Count; i++)
			{
				Color col = originalColors[i];
				col.a = alpha;
				mats[i].SetColor("_Color", col);
			}
		}
	}
}
