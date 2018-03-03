using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    public bool debug = false;

    public float defaultSpeed = 5;
    public float defaultJumpSpeed = 5;

    public float jumpSpeed = 1;
    public float speed = 1;
    public float acceleration = 0.1f;
    public float jumpReaction = 0.5f;

    public LayerMask lmask;
    CapsuleCollider collider;
    private Rigidbody rb;

    bool jump = false;

    private bool active = false;
    private Animator anim;
    private float jumpInput = 0f;
    public float downRay = 0.1f;
    private float scaleX = 1;
    private void OnDrawGizmos()
    {

        if (collider == null || !debug) return;

        Gizmos.color = jump ? Color.green : Color.red;
        Gizmos.DrawCube(transform.position + Vector3.down * collider.height / 2, new Vector3(collider.radius - 0.01f, 0.05f, 0.1f));
        Gizmos.DrawSphere(transform.position + Vector3.down * collider.height / 2, collider.radius);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * downRay);
        Gizmos.DrawWireCube(transform.position + Vector3.left*collider.radius, new Vector3(collider.radius, collider.height-0.1f, 0.1f));
        Gizmos.DrawWireCube(transform.position + Vector3.right*collider.radius, new Vector3(collider.radius, collider.height-0.1f, 0.1f));
    }

    void Start()
    {


        if (anim == null)
            anim = GetComponent<Animator>();
        FindObjectOfType<CameraPosition>().AddTransform(transform);
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        speed = defaultSpeed;
        jumpSpeed = defaultJumpSpeed;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

    }

    public void InputUpdate(PlayerInput playerInput)
    {
        if(playerInput.Jump())
        {
            jumpInput = jumpReaction;
        }
    }

    private bool CheckCollision(Vector3 direction)
    {
        //return Physics.CheckCapsule(
        //    transform.position + Vector3.up * collider.size.y/2,
         //   transform.position + Vector3.down * collider.size.y/2, collider.size.x/2, lmask);
        return Physics.CheckBox(transform.position + direction*collider.radius, new Vector3(collider.radius/4, (collider.height - 0.1f)/2, 0.1f), Quaternion.Euler(-90,0,0), lmask);
           
    }

    public void AffectRenderer(Transform transform, Action<Renderer> action)
    {

        action(transform.GetComponent<Renderer>());

        foreach(Transform t in transform)
        {
            AffectRenderer(t, action);
        }
    }

    public void Activate(bool activated)
    {
        active = activated;
        rb.isKinematic = !activated;
    }

    public void ResetJumpSpeed()
    {
        jumpSpeed = defaultJumpSpeed;
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }

    public void MovementUpdate(PlayerInput playerInput)
    {
        if (!active)
            return;

        if (GameController.EndGame)
            return;

        Vector3 velocity = rb.velocity;
        float hInput = playerInput.Horizontal();
        
        if(jumpInput > 0f)
            jumpInput -= Time.fixedDeltaTime;

        if (Physics.CheckSphere(transform.position + Vector3.down * collider.height/2, collider.radius, lmask) 
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
        if (hInput != 0f)
            scaleX = hInput > 0 ? 1 : -1;

        velocity.x = Mathf.Lerp(velocity.x, hInput * speed, acceleration*Time.fixedDeltaTime);
        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);

        if( velocity.x < 0 
            && CheckCollision(Vector3.left))
            velocity.x = 0;
        if (velocity.x > 0
            && CheckCollision(Vector3.right))
            velocity.x = 0;

        rb.velocity = velocity;

        anim.SetFloat("XVelo", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("YVelo", jump? 0 : rb.velocity.y);

        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, scaleX, acceleration*2* Time.deltaTime), 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Ladder")
        {
            rb.useGravity = false;
            Vector3 velocity = rb.velocity;
            velocity.y = defaultJumpSpeed;
            rb.velocity = velocity;
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
