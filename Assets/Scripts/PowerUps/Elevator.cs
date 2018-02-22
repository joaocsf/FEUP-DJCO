using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : PowerUp {
    public Elevator(GameObject player) : base(player)
    {
        immediateUpdate = true;
    }

    public override void Activate()
    {
        Vector3 test = player.transform.localPosition;
        test.y += (float)4.5;

        player.transform.localPosition = test;
    }

    public override void Update()
    {}
}
