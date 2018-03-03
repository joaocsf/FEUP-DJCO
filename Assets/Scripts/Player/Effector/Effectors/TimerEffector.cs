using UnityEngine;
using System.Collections;
using System;

public abstract class TimerEffector : Effector
{
    public float time = 0;

    protected override void OnInitialize()
    {
        OnInit();
    }

    protected abstract void OnInit();

    protected abstract void OnDelete();

    protected override void OnRemove()
    {
        OnDelete();
        Destroy(this);
    }

    protected override void OnUpdate(float deltaTime)
    {
        time -= deltaTime;
        if(time < 0)
            OnRemove();
    }
}
