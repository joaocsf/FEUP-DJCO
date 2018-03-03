using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effector : ScriptableObject {

    protected PlayerStatus player;

    public void Initialize(PlayerStatus player)
    {
        this.player = player;
        OnInitialize();
    }

    public void Remove()
    {
        OnRemove();
    }

    public void DeltaUpdate(float deltaTime)
    {
        OnUpdate(deltaTime);
    }

    protected abstract void OnInitialize();

    protected abstract void OnRemove();

    protected abstract void OnUpdate(float deltaTime);

}
