using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public bool immediateActivate;
    private bool activated = false;
    protected GameObject player;
    public Sprite sprite;
    protected PlayerStyle style;

    public virtual void Initialize(GameObject player)
    {
        style = player.GetComponent<PlayerStyle>();
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
            activated = OnActivate();
            if (activated)
                style.SetPowerUpSprite(null);
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

    public virtual void OnDeactivate()
    {
        style.SetPowerUpSprite(null);
        Destroy(this);
    }
}
