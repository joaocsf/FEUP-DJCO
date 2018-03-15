using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePowerUpGenerator : MonoBehaviour {
    public PowerUp powerup;
    public float thrust = 3f;

    public ParticleSystem ps;

    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.up * 5);
    }

    private void OnDestroy()
    {
        if (ps == null)
            return;
        ps.transform.parent = null;
        ps.Play();
        ps.transform.eulerAngles = new Vector3(-90, 0, 0);
        Destroy(ps.gameObject, 1f);
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
