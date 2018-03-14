using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorDestoyer : MonoBehaviour, IEffectorApplierEvents
{

    public GameObject[] disable;

    public GameObject[] enable;

    public ParticleSystem[] particles;

    public bool disableCollider = true;

    public float time;

    public float OnDelete()
    {

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().isKinematic = true;

        if (disableCollider)
            GetComponent<Collider>().enabled = false;

        foreach (GameObject obj in enable)
            obj.SetActive(true);

        foreach (GameObject obj in disable)
            obj.SetActive(false);

        foreach (ParticleSystem p in particles)
            p.Play();

        return time;
    }

    public void OnPickup()
    {
    }
}
