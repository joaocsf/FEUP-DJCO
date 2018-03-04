using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraHandler : MonoBehaviour {

    private Camera camera;

    private string globalTexture = "_Global2ndCamera";

    public bool startFirst = false;

    public void GenerateRenderTexture()
    {
        camera = GetComponent<Camera>();
        if(camera.targetTexture != null)
        {
            RenderTexture tmp = camera.targetTexture;
            camera.targetTexture = null;
            DestroyImmediate(tmp);
        }

        camera.targetTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 16);
        camera.targetTexture.filterMode = FilterMode.Bilinear;

        Shader.SetGlobalTexture(globalTexture, camera.targetTexture);
 
    }

	void Start () {
        if (startFirst)
        {
            GenerateRenderTexture();
        }
	}
}
