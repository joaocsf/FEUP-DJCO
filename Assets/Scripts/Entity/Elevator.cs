using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialElevator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
		
			//Destroy(other.gameObject);


		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
