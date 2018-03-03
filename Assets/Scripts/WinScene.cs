using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 1;
    public float walkTime = 3;

    private bool started = false;
    private float startTime;

    public Animator animator;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        StartAnimation();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (started)
        {
            transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        }
        

    }

    IEnumerator StartAnimationAux()
    {
        started = true;
        yield return new WaitForSeconds(walkTime);
        started = false;
        animator.SetBool("Fall", true);
    }

    public void StartAnimation()
    {
        StartCoroutine(StartAnimationAux());
            
    }
}
