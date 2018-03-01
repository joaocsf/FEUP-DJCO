using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class EffectsManager : MonoBehaviour, IPlayerEvents {

    public ParticleSystem born;
    public ParticleSystem dead;

    private void Start()
    {
        GetComponent<PlayerStatus>().AddPlayerEventListener(this);
    }

    public void OnDeActivated()
    {
        dead.Play();
    }

    public void OnActivated()
    {
        born.Play();
    }
}
