using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour{
    public PowerUp[] powerups;

    public PowerUp GetPowerUp(GameObject player)
    {
        Debug.Log("Picked up Manager");
        //Check what is players position in game and generates powerup accordingly
        PowerUp original = powerups[Random.Range(0, powerups.Length)];

        PowerUp p = Object.Instantiate(original) as PowerUp;
        p.Initialize(player);

        if (p.immediateActivate)
        {
            p.Activate();
        }

        return p;
    }
}
