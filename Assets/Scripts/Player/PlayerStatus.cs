using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
    public float floorHeight = 1f;

    private int playerId = 1;
    private int currentFloor = 0;
    private Text text;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        text = GameObject.Find("Player" + playerId + " Text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //Check Floor
        int newFloor = Mathf.FloorToInt(transform.position.y / GameController.floorHeight);
        if (currentFloor != newFloor)
        {
            if (gameController != null)
                gameController.ReportNewFloor(newFloor);
            currentFloor = newFloor;
            UpdateText();
        }
    }


    private void UpdateText()
    {
        text.text = "<b>Player " + playerId + "</b>\n" + currentFloor + "F";
    }
}
