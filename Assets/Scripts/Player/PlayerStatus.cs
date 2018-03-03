using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(PlayerStyle))]
public class PlayerStatus : MonoBehaviour {

    public int playerID = 1;
    private int currentFloor = 0;
    private PlayerUIController controller;
    private GameController gameController;
    public PlayerInput Input { get; private set; }
    private Movement movement;
    private PlayerStyle style;
    private bool active = false;

    
    private List<IPlayerEvents> listeners = new List<IPlayerEvents>();

    public void AddPlayerEventListener(IPlayerEvents listener)
    {
        listeners.Add(listener);
    }
	// Use this for initialization
	IEnumerator Start () {

        movement = GetComponent<Movement>();
        style = GetComponent<PlayerStyle>();
        gameController = GameObject.FindObjectOfType<GameController>();
        Input = InputManager.GetInput(playerID);

        yield return new WaitForFixedUpdate();

        Active(active);
        controller = UIManager.instance.createPlayerUI();
        style.UpdateSortingLayer(transform, playerID);
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

        movement.InputUpdate(Input);

        int newFloor = Mathf.FloorToInt(transform.position.y / GameController.floorHeight);
        if (currentFloor != newFloor)
        {
            if (gameController != null)
                gameController.ReportNewFloor(newFloor);
            currentFloor = newFloor;
            UpdateText();
        }
    }

    private void FixedUpdate()
    {
        if (active)
            //Debug.Log(Input.Horizontal());
        movement.MovementUpdate(Input);
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

    public void SetReady(bool state)
    {
         
    }

    private void CallListeners(Action<IPlayerEvents> action)
    {
        listeners.ForEach(action);
    }

    public void Active(bool state)
    {
        active = state;

        if(state == true)
        {
            CallListeners((listener) => listener.OnActivated());
        }else
            CallListeners((listener) => listener.OnDeActivated());

        foreach(Transform t in transform)
        {
            if(t.GetComponent<ParticleSystem>() == null)
                t.gameObject.SetActive(active);
        }

        movement.Activate(state);
    }

    public bool IsActive()
    {
        return active;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ready")
        {

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ready")
        {

        }
        
    }

}
