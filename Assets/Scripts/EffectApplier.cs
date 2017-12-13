using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectApplier : MonoBehaviour {

	public Material effect;

	protected void nRenderImage(RenderTexture source, RenderTexture destination)
	{
		if(effect != null)
		{
			Graphics.Blit(source, destination, effect);
		} else
		{
			Graphics.Blit(source, destination);
		}
	}
}
