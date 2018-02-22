using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    private int playerId = 1;
    private int currentFloor = 0;
    public PlayerUIController controller;
    public Text timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;



    private GameController gameController;

	// Use this for initialization
	IEnumerator Start () {

        gameController = GameObject.FindObjectOfType<GameController>();
        yield return new WaitForFixedUpdate();
        controller = UIManager.instance.createPlayerUI();
        secondsCount = 0;
        minuteCount = 0;
        hourCount = 0;
        UpdateTimerUI();
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

        UpdateTimerUI();

    }


    private void UpdateText()
    {
        controller.SetScoreText("<b>Player " + playerId + "</b>\n" + currentFloor + "F");
    }



    private void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }
}
