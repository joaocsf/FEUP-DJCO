﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    [HideInInspector] public bool immediateUpdate;
    protected GameObject player;
    public Sprite sprite;

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

    public abstract void Activate();

    public abstract void Update();
}
