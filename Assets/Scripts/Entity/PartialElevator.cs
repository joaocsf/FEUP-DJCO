using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialElevator : MonoBehaviour
{
	public Vector3 offset = Vector3.zero;
	public float height = 4.0f;
	private GameObject player;
	private bool goIn = false;
	private bool exit = false;
	private float displacement = 1.0f;
	private float actualDisplacement = 0.0f;

	public float speed = 5;	
	private float playerZ;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            if (GameController.GetPlayerPosition(other.GetComponent<PlayerStatus>()) < 0.5f)
            {
                player = other.gameObject;
                goIn = true;
				player.GetComponent<Movement>().Activate(false);
				playerZ = player.transform.position.z;
            }
		}
	}
	
	void Update () {
		if (goIn)
		{
			if (actualDisplacement < displacement)
			{
				player.transform.position = Vector3.Lerp(player.transform.position, 
														transform.position + offset, 
														actualDisplacement/displacement);
				actualDisplacement += speed*Time.deltaTime;
			}
			else
			{
				player.transform.position = transform.position + offset + new Vector3(0,height,0);
				goIn = false;
				actualDisplacement = 0.0f;
				exit = true;
			}
		}
		
		if (exit)
		{
			if (actualDisplacement < displacement)
			{
				player.transform.position = Vector3.Lerp(player.transform.position, 
														transform.position + new Vector3(0,height + offset.y,playerZ), 
														actualDisplacement/displacement);
				actualDisplacement += speed*Time.deltaTime;
			}
			else
			{
				player.transform.position = transform.position + new Vector3(0,height + offset.y, playerZ);
				player.GetComponent<Movement>().Activate(true);
				goIn = false;
				actualDisplacement = 0.0f;
				exit = false;
				player = null;
			}
		}
	}

}
