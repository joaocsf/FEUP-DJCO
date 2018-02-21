using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    private int playerId = 1;
    private int currentFloor = 0;
    public PlayerUIController controller;

    private GameController gameController;

	// Use this for initialization
	IEnumerator Start () {

        gameController = GameObject.FindObjectOfType<GameController>();
        yield return new WaitForFixedUpdate();
        controller = UIManager.instance.createPlayerUI();
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
        controller.SetScoreText("<b>Player " + playerId + "</b>\n" + currentFloor + "F");
    }
}
