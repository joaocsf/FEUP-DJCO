using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public PlayerStatus playerStatus;
    public bool enabled = false;

	void Update () {
        
        if(playerStatus.Input.Jump() && !enabled)
        {
            enabled = true;
            playerStatus.gameObject.transform.position = transform.position;
            playerStatus.Active(true);
        }
        if(playerStatus.Input.Fire() && enabled)
        {
            enabled = false;
            playerStatus.Active(false);
        }
        
	}
}
