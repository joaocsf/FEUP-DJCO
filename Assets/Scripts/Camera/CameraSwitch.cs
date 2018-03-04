using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraFX))]
public class CameraSwitch : MonoBehaviour {

    public Camera secondaryCamera;
    private Camera mainCamera;
    public CameraFX cameraFX;

    public int iter = 10;
    public float time = 10;

    public bool secondCamera = true;
    private bool oldSecondCamera;

    public void Start()
    {
        oldSecondCamera = secondCamera;
        cameraFX = GetComponent<CameraFX>();
        mainCamera = GetComponent<Camera>();
        if (secondCamera)
        {
            cameraFX.Transition(1);
        }
    }

    void Update () {
        if(secondCamera != oldSecondCamera)
        {
            StartCoroutine(AnimSwap());
            oldSecondCamera = secondCamera;
        }
	}

    IEnumerator UpdateState(float reverse)
    {
        for(int i = 0; i <= iter; i++)
        {
            cameraFX.Transition(Mathf.Abs((float)i / iter - reverse));
            yield return new WaitForSeconds(time / iter);
        }
    }

    IEnumerator AnimSwap()
    {
        if (secondCamera)
        {
            secondaryCamera.enabled = true;
            secondaryCamera.GetComponent<CameraHandler>().GenerateRenderTexture();
            yield return StartCoroutine(UpdateState(0));
        }
        else
        {
            yield return StartCoroutine(UpdateState(1));
            secondaryCamera.enabled = false;
        }
    }
}
