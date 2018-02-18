using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Check what is players position in game and generates powerup accordingly
            PowerUp p = new Elevator();

            if (p.immediateUpdate)
            {
                p.Activate();
            }
            else
            {
                //Save powerups somewhere to use later
            }
        }
    }
}
