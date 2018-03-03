using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Firework : MonoBehaviour {

    public float maxVelocity = 5f;
    public float acceleration = 2f;
    public GameObject replacement;
    public GameObject renderer;
    public ParticleSystem explosion;
    private bool active = true;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        if (!active)
            return;

        Vector3 velo = rb.velocity;
        velo.x = velo.z = 0;
        velo.y = Mathf.Clamp(rb.velocity.y + acceleration*Time.fixedDeltaTime, 0f, maxVelocity);
        rb.velocity = velo;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStatus>() != null || other.tag == "Ladder")
            return;

        active = false;
        GameObject.FindObjectOfType<CameraPosition>().AddTrauma(0.2f);

        if(other.tag == "Destructible")
        {
            Transform parent = other.transform.parent;
            Vector3 position = other.transform.position;
            Vector3 rotation = other.transform.eulerAngles;

            Instantiate(replacement, position, Quaternion.Euler(rotation), parent);
            Destroy(other.gameObject);
        }


        renderer.SetActive(false);
        rb.velocity = Vector3.zero;
        transform.position = other.transform.position;
        explosion.Play();
        Destroy(gameObject, 1);
        
    }
}
