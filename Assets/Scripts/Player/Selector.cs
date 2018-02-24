using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public Movement movement;
    public bool enabled = false;

	void Update () {
        
        if(Input.GetButtonDown("Jump" + movement.playerID) && !enabled)
        {
            enabled = true;
            Debug.Log("HERE" + movement.playerID);
            movement.gameObject.transform.position = transform.position;
            movement.CanControll(true);
        }
        if(Input.GetButtonDown("Fire" + movement.playerID) && enabled)
        {
            enabled = false;
            Debug.Log("THERE" + movement.playerID);
            movement.CanControll(false);
        }
        
	}
}
