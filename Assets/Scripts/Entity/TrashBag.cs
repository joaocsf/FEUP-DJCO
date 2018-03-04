﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour {

    public float thrust;
    public float stunTime;
    private bool lastGrounded;
    private float distToGround;
    private bool thrown = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        lastGrounded = IsGrounded();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.6));
    }

    IEnumerator StunPlayer(Movement movement)
    {
        float oldSpeed = movement.speed;
        float oldJump = movement.jumpSpeed;
        
        movement.speed = 0;
        movement.jumpSpeed = 0;
        yield return new WaitForSeconds(stunTime);
        movement.speed = oldSpeed;
        movement.jumpSpeed = oldJump;

        Destroy(gameObject, 0);
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        if(lastGrounded != isGrounded)
        {
            if (isGrounded)
            {
                int rand = Random.Range(0, 2)*2 - 1; // -1 or 1
                thrust = rand * thrust;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            lastGrounded = !lastGrounded;
        }

        if (isGrounded)
        {
            Vector3 position = gameObject.transform.position;
            position.y = (float)(position.y + 0.2);
            rb.AddForceAtPosition(Vector3.right * thrust, position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStatus>() != null)
        {
            if (!thrown)
            {
                thrown = true;
            }
            else
            {
                StartCoroutine(StunPlayer(other.GetComponent<Movement>()));
            }
        }
        if(other.tag == "Wall")
        {
            thrust = -thrust;
        }
    }
}
