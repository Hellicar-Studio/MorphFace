using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectApplier : MonoBehaviour {

	public Material effect;

	[Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
	public Camera foregroundCamera;

	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("Kinect joint that is going to be overlayed.")]
	public KinectInterop.JointType trackedJoint = KinectInterop.JointType.HandRight;

	[Tooltip("Smoothing factor used for joint rotation.")]
	public float smoothFactor = 10f;

	public float yOffset;

	private KinectManager manager;

	protected void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if(manager == null)
		{
			manager = KinectManager.Instance;
		}
		if(manager && manager.IsInitialized())
		{
			Rect backgroundRect = foregroundCamera.pixelRect;
			long userId = manager.GetUserIdByIndex(playerIndex);

			int iJointIndex = (int)trackedJoint;

			if (manager.IsJointTracked(userId, iJointIndex))
			{
				Vector2 posJoint = manager.GetJointPosColorOverlay(userId, iJointIndex, backgroundRect);
				posJoint.y += yOffset;
				posJoint.x /= backgroundRect.width;
				posJoint.y /= backgroundRect.height;
				posJoint.x *= 16.0f / 9.0f;
				if (posJoint != Vector2.zero)
				{
					effect.SetVector("_Center", new Vector4(posJoint.x, posJoint.y, 0.0f, 0.0f));
				}
			}
		}

		if (effect != null)
		{
			Graphics.Blit(source, destination, effect);
		} else
		{
			Graphics.Blit(source, destination);
		}
	}
}
