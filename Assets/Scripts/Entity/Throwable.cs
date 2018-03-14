using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour, IEffectorApplierEvents {

    public Vector2 velocity;
    public Transform rotateObj;

    public float rotationFactor = 0f;
    private Rigidbody rb;
    public bool useDirection = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(float dir)
    {
        if(useDirection)
            velocity.x *= Mathf.Sign(dir);
    }

    void FixedUpdate () {

        if (rotateObj)
        {
            Vector3 euler = rotateObj.localEulerAngles;
            euler.z += Mathf.Sign(velocity.x) * Time.deltaTime * rotationFactor;
            rotateObj.localEulerAngles = euler;
        }

        rb.velocity = velocity;	
	}

    public float OnDelete()
    {
        rotateObj = null;
        return 0f;
    }

    public void OnPickup()
    {
    }
}
