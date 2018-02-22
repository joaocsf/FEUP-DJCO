using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPlayer : MonoBehaviour
{
    public AudioClip[] effects;
    public AudioSource audioSource;

    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha6))
        {
            if(effects.Length > 0)
            {
                audioSource.clip = effects[Random.Range(0, effects.Length)];
                audioSource.Play();
            }
        }
    }
}
