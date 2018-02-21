using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject playerUI;
    public Transform playerSlots;

	void Start () {
        instance = this;	
	}

    public PlayerUIController createPlayerUI()
    {
        return Instantiate(playerUI, playerSlots).GetComponent<PlayerUIController>();
    }
}
