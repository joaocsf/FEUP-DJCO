using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public enum GameState
    {
        Selection,
        Playing,
        Win,
        Credits
    }

    public static float floorHeight
    {
        get
        {
            return generator.floorHeight;
        }
        private set { }
    }

    public static float floorWidth
    {
        get
        {
            return generator.floorWidth;
        }
        private set { }
    }

    public static float tilesNumber
    {
        get
        {
            return generator.tilesNumber;
        }
        private set { }
    }
    public static int highestFloor = 0;
    public static GameState gameState;

    private static Generator generator;
    private static CameraPosition cameraPosition;

    // Use this for initialization
    void Start () {
        gameState = GameState.Selection;

        generator = GameObject.FindObjectOfType<Generator>();
        cameraPosition = GameObject.FindObjectOfType<CameraPosition>();
    }
	
	public void ReportNewFloor(int newFloor)
    {
        if(highestFloor < newFloor)
        {
            highestFloor = newFloor;
            generator.SetHighestFloor(highestFloor);
            cameraPosition.SetHighestFloor(highestFloor);
            Debug.Log(highestFloor);
        }
    }
}
