using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 initial = transform.localPosition;
        transform.localPosition = (new Vector3(initial.x, initial.y, 0.25f + 0.25f * Mathf.Cos(Time.time)));
      
    }
}
