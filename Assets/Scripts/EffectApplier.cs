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

	private float[] faces;

	private KinectManager manager;

	private void Awake()
	{
		effect.SetFloatArray("_Faces", new float[12]);
		faces = new float[12];
	}

	protected void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if(manager == null)
		{
			manager = KinectManager.Instance;
		}
		if(manager && manager.IsInitialized())
		{
			for(int i = 0; i < 12; i+=2)
			{
				Rect backgroundRect = foregroundCamera.pixelRect;
				long userId = manager.GetUserIdByIndex(i/2);

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
						if(faces[i] == 100 && faces[i+1] == 100)
						{
							faces[i] = posJoint.x;
							faces[i + 1] = posJoint.y;
						} else
						{
							faces[i] = Mathf.Lerp(faces[i], posJoint.x, smoothFactor);
							faces[i+1] = Mathf.Lerp(faces[i+1], posJoint.y, smoothFactor);

						}

					}
					else
					{
						faces[i] = 100;
						faces[i + 1] = 100;
					}
				}
				else
				{
					faces[i] = 100;
					faces[i + 1] = 100;
				}
			}
		}

		//effect.SetVector("_Center", new Vector4(posJoint.x, posJoint.y, 0.0f, 0.0f));
		effect.SetFloatArray("_Faces", faces);

		if (effect != null)
		{
			Graphics.Blit(source, destination, effect);
		} else
		{
			Graphics.Blit(source, destination);
		}
	}
}
