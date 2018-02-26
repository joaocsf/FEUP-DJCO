using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    private int playerId = 1;
    private int currentFloor = 0;
    private PlayerUIController controller;
    private GameController gameController;
    


	// Use this for initialization
	IEnumerator Start () {

        gameController = GameObject.FindObjectOfType<GameController>();
        yield return new WaitForFixedUpdate();
        controller = UIManager.instance.createPlayerUI();
    }

    public void UIEnabled(bool v)
    {
        if(controller != null)
            controller.gameObject.SetActive(v);
    }

    public void SetHeadSprite(Sprite s)
    {
        if (controller != null)
            controller.SetHeadSprite(s);
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
        if (currentFloor + 3 < GameController.cameraFloor)
        {
            controller.SetScoreText("END GAME");
            GameController.EndGame = true;
        }
        else
            controller.SetScoreText(currentFloor >= 0 ? currentFloor.ToString() : "");

    }
}
