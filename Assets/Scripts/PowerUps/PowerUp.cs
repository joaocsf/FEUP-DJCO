using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp
{
    [HideInInspector] public bool immediateUpdate;

    public abstract void Activate(GameObject player);
}
