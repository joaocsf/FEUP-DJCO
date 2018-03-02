using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialElevator : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			GameObject player = other.gameObject;
			Vector3 velocida= new Vector3(0, 4, 0);
			player.transform.Translate(velocida);
		}
	}

}
