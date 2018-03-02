using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public bool immediateActivate;
    private bool activated = false;
    protected GameObject player;
    public Sprite sprite;

    public virtual void Initialize(GameObject player)
    {
        this.player = player;
        if (immediateActivate)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (!activated)
        {
            activated = true;
            OnActivate();
        }
    }

    public void UpdateNow(float deltaTime)
    {
        if (activated)
        {
            OnUpdate(deltaTime);
        }
    }

    protected abstract void OnUpdate(float deltaTime);

    protected abstract bool OnActivate();
}
