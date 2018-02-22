using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : PowerUp {
    public Elevator()
    {
        immediateUpdate = true;
    }

    // Use this for initialization
    void Start () {
        immediateUpdate = true;
	}

    // Update is called once per frame
    void Update () {}

    public override void Activate(GameObject player)
    {
        Vector3 test = player.transform.localPosition;
        test.y += (float)4.5;

        player.transform.localPosition = test;
    }
}
