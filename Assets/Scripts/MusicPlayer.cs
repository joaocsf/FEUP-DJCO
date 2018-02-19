using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public AudioClip selectionMusic;
    public AudioClip playingMusic;
    public AudioClip rushMusic;
    public AudioClip winMusic;
    public AudioClip creditsMusic;
    public AudioSource musicSource;

 
    // Use this for initialization
    void Start()
    {
       

    }



    void Update()
    {
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            musicSource.clip = selectionMusic;
            musicSource.Play();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            musicSource.clip = playingMusic;
            musicSource.Play();
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            musicSource.clip = rushMusic;
            musicSource.Play();
        }
        else if(Input.GetKey(KeyCode.Alpha4))
        {
            musicSource.clip = winMusic;
            musicSource.Play();
        }
        else if(Input.GetKey(KeyCode.Alpha5))
        {
            musicSource.clip = creditsMusic;
            musicSource.Play();
        }
    }
}
