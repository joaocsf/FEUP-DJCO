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


    public void UpdateMusic(GameController.GameState state)
    {
        switch (state)
        {
            case GameController.GameState.Menu:
                credits.TransitionTo(transition);
                break;
            case GameController.GameState.Credits:
                credits.TransitionTo(transition);
                break;
            case GameController.GameState.Playing:
                racing.TransitionTo(transition);
                break;
            case GameController.GameState.Selection:
                selection.TransitionTo(transition);
                break;
            case GameController.GameState.Win:
                win.TransitionTo(transition);
                break;
        }
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
