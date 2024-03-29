﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    private List<Transform> transforms = new List<Transform>();

    private List<ICameraEvents> listeners = new List<ICameraEvents>();

    [Range(0,1)]
    public float trauma = 0f;
    public float traumaScale = 2;
    public float traumaDecay = 0.5f;
    public float traumaAngle = 45;
    public float traumaIntensity = 0.1f;
    public float smoothRatio = 2f;

    public Vector3 compensation = Vector3.zero;

    private Vector3 targetPos = Vector3.zero;

    private int highestFloor = 0;

    public void AddListener(ICameraEvents listener)
    {
        listeners.Add(listener);
    }
    
    public void AddTransform(Transform t)
    {
        transforms.Add(t);
    }

    public void SetHighestFloor(int floor)
    {
        highestFloor = floor;
        listeners.ForEach((x) => x.OnCameraMove());
    }

    public void AddTrauma(float ammount)
    {
        trauma = Mathf.Clamp(trauma + ammount, 0, 1f);
    }


    void Update() {

        trauma = Mathf.Clamp(trauma - Time.deltaTime*traumaDecay, 0f, 1f);

        Vector3 center = Vector3.zero;

        center.x = GameController.floorWidth * GameController.tilesNumber / -2;
        center.y = highestFloor * GameController.floorHeight;

        Vector3 traumaV = Vector3.zero;

        traumaV.x += trauma*trauma * (1f - Mathf.PerlinNoise(Time.time*traumaScale, 0.1f) * 2);
        traumaV.y += trauma*trauma * (1f - Mathf.PerlinNoise(Time.time*traumaScale, 0.9f) * 2);
        transform.localEulerAngles = new Vector3(10, 0, traumaIntensity * trauma * trauma * (1f - Mathf.PerlinNoise(Time.time*traumaScale, 0.1f) * 2) * traumaAngle);
        traumaV *= traumaIntensity;
        targetPos = center + compensation;
        
        transform.position = traumaV + Vector3.Lerp(transform.position, targetPos, Time.deltaTime* smoothRatio);

	}
}
