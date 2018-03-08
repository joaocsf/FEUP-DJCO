using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePowerUpGenerator : MonoBehaviour {
    public PowerUp powerup;
    public float thrust = 3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

        public PowerUp GetPowerUp(GameObject player)
    {
        PlayerStatus ps = player.GetComponent<PlayerStatus>();

        PowerUp p = Object.Instantiate(powerup) as PowerUp;
        p.Initialize(player);

        if (p.immediateActivate)
        {
            p.Activate();
        }

        return p;
    }

    void FixedUpdate()
    {
            rb.velocity = Vector3.right * thrust;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Wall")
        {
            thrust = -thrust;
        }
    }
}
