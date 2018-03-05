using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorDestoyer : MonoBehaviour, IEffectorApplierEvents
{

    public GameObject[] disable;

    public ParticleSystem[] particles;

    public bool disableCollider = true;

    public float time;

    public bool OnDelete()
    {

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().isKinematic = true;

        if (disableCollider)
            GetComponent<Collider>().enabled = false;
        Debug.Log(GetComponent<Collider>().enabled = false);

        foreach (GameObject obj in disable)
            obj.SetActive(false);

        foreach (ParticleSystem p in particles)
            p.Play();

        Destroy(gameObject, time);

        return false;

    }

    public void OnPickup()
    {
    }
}
