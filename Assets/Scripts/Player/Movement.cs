using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Movement : MonoBehaviour {

    public bool debug = false;
    public int playerID = 0;

    public float defaultSpeed = 5;
    public float defaultJumpSpeed = 5;

    public float jumpSpeed = 1;
    public float speed = 1;
    public float acceleration = 0.1f;
    public float jumpReaction = 0.5f;

    public LayerMask lmask;
    BoxCollider collider;
    private Rigidbody rb;
    bool jump = false;


    private float jumpInput = 0f;

    public float downRay = 0.1f;

    private bool bool1 = false;
    private bool bool2 = false;

    private void OnDrawGizmos()
    {

        if (collider == null || !debug) return;

        Gizmos.color = jump ? Color.green : Color.red;
        Gizmos.DrawCube(transform.position + Vector3.down * collider.size.y / 2, new Vector3(collider.size.x - 0.01f, 0.05f, collider.size.z / 2));
        Gizmos.DrawSphere(transform.position + Vector3.down * collider.size.y / 2, collider.size.x / 2);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * downRay);
        Gizmos.color = bool1 ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.left*collider.size.x/2, new Vector3(collider.size.x/2, collider.size.y-0.1f, collider.size.z));
        Gizmos.color = bool2 ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right*collider.size.x/2, new Vector3(collider.size.x/2, collider.size.y-0.1f, collider.size.z));
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        speed = defaultSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump" + playerID))
        {
            jumpInput = jumpReaction;
        }
    }


    private bool CheckCollision(Vector3 direction)
    {
        //return Physics.CheckCapsule(
        //    transform.position + Vector3.up * collider.size.y/2,
         //   transform.position + Vector3.down * collider.size.y/2, collider.size.x/2, lmask);
        return Physics.CheckBox(transform.position + direction*collider.size.x/2, new Vector3(collider.size.x/4, (collider.size.y - 0.1f)/2, collider.size.z/2), Quaternion.Euler(-90,0,0), lmask);
           
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        float hInput = Input.GetAxisRaw("Horizontal" + playerID);
        
        if(jumpInput > 0f)
            jumpInput -= Time.fixedDeltaTime;

        if (Physics.CheckSphere(transform.position + Vector3.down * collider.size.y/2, collider.size.x/2, lmask) 
            && Mathf.Abs(velocity.y) < 0.1f)
        {
            jump = true;
        }
        else
            jump = false;

        if(jumpInput > 0f && jump)
        {
            jumpInput = -1f;
            jump = false;
            velocity.y = jumpSpeed;
        }
        
        velocity.x = Mathf.Lerp(velocity.x, hInput * speed, acceleration*Time.fixedDeltaTime);
        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);

        if( velocity.x < 0 
            && CheckCollision(Vector3.left))
            velocity.x = 0;
        if (velocity.x > 0
            && CheckCollision(Vector3.right))
            velocity.x = 0;

        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Ladder")
        {
            rb.useGravity = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ladder")
        {
            rb.useGravity = true;
        }
    }

}
