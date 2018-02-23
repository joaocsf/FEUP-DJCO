using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : PowerUp {
    public Elevator(GameObject player) : base(player)
    {
        immediateUpdate = false;
    }

    public override void Activate()
    {
        Vector3 test = player.transform.localPosition;
        test.y += (float)4.5;

        player.transform.localPosition = test;

        this.isOver = true;
    }

    public override void Update()
    {}
}
