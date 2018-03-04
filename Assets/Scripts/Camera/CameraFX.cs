using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour{

	public Material mat;

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit(src, dest, mat);
	}

    public void Transition(float f)
    {
        mat.SetFloat("_Lerp", f);
    }
}
