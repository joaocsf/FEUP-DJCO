using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorDestoyer : MonoBehaviour, IEffectorApplierEvents
{

    public GameObject[] disable;

    public ParticleSystem[] particles;

    public float time;

    public bool OnDelete()
    {

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().isKinematic = true;

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
