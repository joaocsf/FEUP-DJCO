using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "PowerUps/Teleport", order = 1)]
public class Teleport : PowerUp
{
    public float deltaY = 6.0f;
    
    protected override bool OnActivate()
    {
        Vector3 newPosition = this.player.transform.position;
        newPosition.y += deltaY;

        player.transform.position = newPosition;

        OnDeactivate();

        return true;
    }

    protected override void OnUpdate(float deltaTime){}
}
