using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject playerUI;
    public Transform playerSlots;
    public Animator menuAnimator;
    public Text text;

    private int score = 0;
    private bool showScore = false;
    private float lerp = 0f;
    public float scoreRate = 0f;
	void Start () {
        instance = this;	
	}

    private void Update()
    {
        if (showScore)
        {
            text.text = (int)Mathf.Lerp(0, score, Mathf.Pow(lerp,2)) + "";
            lerp += Time.deltaTime * scoreRate;
            if (lerp >= 1)
                showScore = false;
        }
        
    }

    public void UpdateScore(int maxFloor)
    {
        lerp = 0;
        showScore = true;
        score = maxFloor;
    }

    public PlayerUIController createPlayerUI()
    {
        return Instantiate(playerUI, playerSlots).GetComponent<PlayerUIController>();
    }

    public void ExitMenu()
    {
        menuAnimator.SetBool("exit", true); 
    }

    public void ShowScoreMenu()
    {

    }
}
