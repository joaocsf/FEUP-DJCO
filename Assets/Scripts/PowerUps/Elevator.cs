﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elevator", menuName = "PowerUps/Elevator", order = 1)]
public class Elevator : PowerUp {

    protected override void OnActivate()
    {
        Vector3 test = player.transform.localPosition;
        test.y += (float)4.5;

        player.transform.localPosition = test;

        Destroy(this);
    }

    public override void OnUpdate(float deltaTime)
    {}
}