using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraFX))]
public class CameraSwitch : MonoBehaviour {

    public Camera secondaryCamera;
    private Camera mainCamera;
    public CameraFX cameraFX;

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
            oldSecondCamera = secondCamera;
            Debug.Log(secondCamera + " " + oldSecondCamera);
            StartCoroutine(AnimSwap());
        }
	}

    IEnumerator UpdateState(float reverse)
    {
        float currTime = 0;
        while(currTime <= time){
            currTime += Time.fixedDeltaTime;
            float weight = Mathf.Clamp(currTime/time, 0f, 1f);            

            cameraFX.Transition(Mathf.Abs(weight - reverse));

            yield return new WaitForFixedUpdate();
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
