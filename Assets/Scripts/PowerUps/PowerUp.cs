﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [HideInInspector] public bool immediateUpdate;

    public abstract void Activate();
}
