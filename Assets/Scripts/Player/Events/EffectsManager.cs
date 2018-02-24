using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class EffectsManager : MonoBehaviour, IPlayerEvents {

    public ParticleSystem born;
    public ParticleSystem dead;

    private void Start()
    {
        GetComponent<Movement>().AddPlayerEventListener(this);
    }

    public void OnControllDisabled()
    {
        dead.Play();
    }

    public void OnControllEnabled()
    {
        born.Play();
    }
}
