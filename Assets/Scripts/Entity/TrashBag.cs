using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour {

    public float thrust;
    private bool lastGrounded;
    private float distToGround;
    private bool firstThrow = false;
    private int dir;
    public LayerMask mask;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        lastGrounded = IsGrounded();
        if (lastGrounded)
        {
            firstThrow = true;
        }
    }

    public void SetDirection(float dir)
    {
        this.dir = (int)(-Mathf.Sign(dir));

        thrust = this.dir * thrust;
        if (!firstThrow)
            this.dir = -1;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.6), mask);
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        if(lastGrounded != isGrounded)
        {
            if (isGrounded)
            {
                if (!firstThrow)
                {
                    dir = -1; //zig-zag
                    firstThrow = true;
                }
                else
                {
                    thrust = dir * thrust;
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            lastGrounded = !lastGrounded;
        }

        if (isGrounded)
        {
            rb.velocity = Vector3.right * thrust + Vector3.up * rb.velocity.y;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Wall")
        {
            thrust = -thrust;
        }
    }
}
