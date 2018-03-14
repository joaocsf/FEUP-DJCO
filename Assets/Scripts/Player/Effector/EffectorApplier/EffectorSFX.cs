using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class EffectorSFX : MonoBehaviour, IEffectorApplierEvents
{

    public AudioClip destroyClip;
    public AudioClip startClip;
    public bool loop = true;
    public float pitchHigh = 1;
    public float pitchLow = 1;
    private AudioSource src;

    public float time;

    public void Start()
    {
        src = GetComponent<AudioSource>();
        src.pitch = (UnityEngine.Random.Range(pitchHigh, pitchLow));
        src.loop = loop;
        src.clip = startClip;
        src.Play();
    }

    public float OnDelete()
    {
        src.pitch = (UnityEngine.Random.Range(pitchHigh, pitchLow));
        src.loop = false;
        src.clip = destroyClip;
        src.Play();
        return time;
    }

    public void OnPickup()
    {

    }
}
