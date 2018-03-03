using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "MovementEffector", menuName = "Effector/MovementEffector", order = 1)]
public class MovementEffector : TimerEffector
{
    public float speedMult = 1f;
    public float jumpMult = 1f;
    Movement movement; 
    protected override void OnInit()
    {
        movement = player.GetComponent<Movement>();
        movement.speed *= speedMult;
        movement.jumpSpeed *= jumpMult;
        if(speedMult < 1 || jumpMult < 1)
        {
            player.GetComponent<PlayerStyle>().SetConfused(true);
        }
    }

    protected override void OnDelete()
    {
        movement.ResetJumpSpeed();
        movement.ResetSpeed();
        
        player.GetComponent<PlayerStyle>().SetConfused(false);
    }
}
