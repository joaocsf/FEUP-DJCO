using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(PlayerStyle))]
public class PlayerStatus : MonoBehaviour, ICameraEvents {

    public int playerID = 1;
    private int currentFloor = 0;
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
        FindObjectOfType<CameraPosition>().AddListener(this);
        movement = GetComponent<Movement>();
        style = GetComponent<PlayerStyle>();
        gameController = GameObject.FindObjectOfType<GameController>();
        Input = InputManager.GetInput(playerID);

        yield return new WaitForFixedUpdate();
        active = !active;
        Active(!active);
        style.UpdateSortingLayer(transform, playerID);
    }

    // Update is called once per frame
    void Update () {

        if (!active) return;

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
            movement.MovementUpdate(Input);
    }

    private void UpdateText()
    {
        if (currentFloor + 3 < GameController.cameraFloor)
        {
            GameController.PlayerEliminated(this);
            Active(false);
        }
    }

    private void CallListeners(Action<IPlayerEvents> action)
    {
        listeners.ForEach(action);
    }

    public void Active(bool state)
    {
        if (active == state)
            return;

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

    public void OnCameraMove()
    {
        if (!active) return;

        int newFloor = Mathf.FloorToInt(transform.position.y / GameController.floorHeight);
        currentFloor = newFloor;
        UpdateText();
    }
}
