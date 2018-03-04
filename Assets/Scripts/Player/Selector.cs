using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public PlayerStatus playerStatus;
    public bool enable = false;

	void Update () {
        
        if(playerStatus.Input.Jump() && !enable)
        {
            enable = true;
            playerStatus.gameObject.transform.position = transform.position;
            playerStatus.Active(true);
        }
        if(playerStatus.Input.Fire() && enable)
        {
            enable = false;
            playerStatus.Active(false);
        }
        
	}
}
