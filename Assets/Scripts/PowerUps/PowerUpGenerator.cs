﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator{

    public PowerUp GetPowerUp(GameObject player)
    {
        Debug.Log("Picked up Manager");
        //Check what is players position in game and generates powerup accordingly
        PowerUp p = new Elevator(player);

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
