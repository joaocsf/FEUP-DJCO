using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp
{
    [HideInInspector] public bool immediateUpdate;
    [HideInInspector] public bool isOver;
    protected GameObject player;

    public PowerUp(GameObject player)
    {
        this.player = player;
        this.isOver = false;
    }

    public abstract void Activate();

    public abstract void Update();
}
