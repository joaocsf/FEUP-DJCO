using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp
{
    [HideInInspector] public bool immediateUpdate;
    protected GameObject player;

    public PowerUp(GameObject player)
    {
        this.player = player;
    }

    public abstract void Activate();

    public abstract void Update();
}
