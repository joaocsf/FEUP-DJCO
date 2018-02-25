using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementBoost", menuName = "PowerUps/MovementBoost", order = 1)]
public class MovementBoost : PowerUp
{
    public float speed;
    public float jumpSpeed;
    public float effectTime;
    private float elapsedTime = 0;

    protected override void OnActivate()
    {
        //TODO Change player speed
        Movement m = player.GetComponent<Movement>();
        m.speed = speed;
        m.jumpSpeed = jumpSpeed;
    }

    public override void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
        Debug.Log("Got here " + elapsedTime);
        if (elapsedTime >= effectTime)
        {
            Movement m = player.GetComponent<Movement>();
            m.ResetSpeed();
            m.ResetJumpSpeed();

            Destroy(this);
        }
    }
}
