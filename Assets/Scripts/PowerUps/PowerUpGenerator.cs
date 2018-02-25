using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour{
    public PowerUp[] powerups;

    public PowerUp GetPowerUp(GameObject player)
    {
        Debug.Log("Picked up Manager");
        //Check what is players position in game and generates powerup accordingly
        PowerUp original = powerups[0/*Random*/];

        PowerUp p = Object.Instantiate(original) as PowerUp;
        p.setPlayer(player);

        if (p.immediateUpdate)
        {
            p.Activate();
            return null;
        }
        else
        {
            return p;
        }
    }
}
