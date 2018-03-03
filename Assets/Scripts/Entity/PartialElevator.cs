using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialElevator : MonoBehaviour
{
	private GameObject player;
	private bool goIn = false;
	private bool exit = false;
	private float displacement = 1.0f;
	private float actualDisplacement = 0.0f;
	


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            if (GameController.GetPlayerPosition(other.GetComponent<PlayerStatus>()) < 0.5f)
            {
                player = other.gameObject;
                goIn = true;
            }
		}
	}
	
	void Update () {
		if (goIn)
		{
			if (actualDisplacement < displacement)
			{
				Vector3 translate = new Vector3(0, 0, 0.05f);
				player.transform.Translate(translate);
				actualDisplacement += 0.05f;
			}
			else
			{
				Vector3 translate= new Vector3(0, 4.2f, 0);
				player.transform.Translate(translate);
				goIn = false;
				actualDisplacement = 0.0f;
				exit = true;
			}
		}
		
		if (exit)
		{
			if (actualDisplacement < displacement)
			{
				Vector3 translate = new Vector3(0, 0, -0.05f);
				player.transform.Translate(translate);
				actualDisplacement += 0.05f;	
			}
			else
			{
				goIn = false;
				actualDisplacement = 0.0f;
				exit = false;
				player = null;
			}
		}
	}

}
