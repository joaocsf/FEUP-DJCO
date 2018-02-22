using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour {
    public AudioMixerSnapshot selection;
    public AudioMixerSnapshot racing;
    public AudioMixerSnapshot rush;
    public AudioMixerSnapshot win;
    public AudioMixerSnapshot credits;
    public float bpm = 128;
    public int transition = 1;

    
    void Start()
    {
 
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selection.TransitionTo(transition);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            racing.TransitionTo(transition);
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            rush.TransitionTo(transition);
        }
        else if(Input.GetKey(KeyCode.Alpha4))
        {
            win.TransitionTo(transition);
        }
        else if(Input.GetKey(KeyCode.Alpha5))
        {
            credits.TransitionTo(transition);
        }
    }
}
