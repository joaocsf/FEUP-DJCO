using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour {

    public float thrust;
    public float stunTime;
    private bool lastGrounded;
    private float distToGround;
    private bool thrown = false;
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
            thrown = true;
        }
    }

    public void SetDirection(float dir)
    {
        this.dir = (int)(-Mathf.Sign(dir));
        thrown = false;
        thrust = this.dir * thrust;
        if (thrown)
            this.dir = -1;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.6), mask);
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
                if (!thrown)
                {
                    dir = -1; //zig-zag
                    thrown = true;
                }
                else
                {
                    //randDir = Random.Range(0, 2) * 2 - 1; // -1 or 1
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
