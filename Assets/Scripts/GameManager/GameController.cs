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

    public GameState state, oldState;

    public static float floorHeight
    {
        get
        {
            return generator.floorHeight;
        }
        private set { }
    }
    public static int cameraFloor
    {
        get
        {
            return floor;
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

    public static  bool EndGame
    {
        get {return endGame; }
        set { UpdateEndGame(value); }
    }

    private static PlayerStatus[] players;

    public static int highestFloor = 0;
    private static GameState gameState;
    private static GameObject selectionBehaviours;
    private static GameObject playerUI;
    private static MusicPlayer musicPlayer;
    private static int floor;
    private static bool endGame = false;

    public static GameState State
    {
        get { return gameState; }
        set { UpdateState(value); }
    }

    private static Generator generator;
    private static CameraPosition cameraPosition;
    private static List<PlayerStatus> runningPlayers;

    private static void UpdateState(GameState state)
    {
        gameState = state;
        selectionBehaviours.SetActive(false);

        musicPlayer.UpdateMusic(state);
        switch (state)
        {
            case GameState.Selection:
                selectionBehaviours.SetActive(true);
                break;
            case GameState.Playing:
                break;
            case GameState.Win:
                break;
            case GameState.Credits:
                break;
        }
    }
    public static void UpdateEndGame(bool value)
    {
        endGame = value;
    }

    void Start () {
        gameState = GameState.Selection;
        selectionBehaviours = GameObject.FindObjectOfType<Selector>().transform.parent.gameObject;
        generator = GameObject.FindObjectOfType<Generator>();
        cameraPosition = GameObject.FindObjectOfType<CameraPosition>();
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        oldState = state;
        State = state;
        players = FindObjectsOfType<PlayerStatus>();
    }

    void Update()
    {
        if(oldState != state)
        {
            oldState = state;
            State = state;
        }
    }

    private static int EnabledPlayers()
    {
        int res = 0;
        foreach(PlayerStatus status in players)
            res += status.IsActive() ? 1 : 0;
        return res;
    }

    public static float GetPlayerPosition(PlayerStatus player)
    {
        float i = 1;
        float total = 0;
        foreach (PlayerStatus s in runningPlayers)
            if (s.IsActive())
            {
                total++;
                if (s != player
                    && s.transform.position.y > player.transform.position.y)
                    i++;
            }
        return 1f - i/total;
    }

    public static bool CheckBeginGame(HashSet<PlayerStatus> readyPlayers)
    {
        if (State != GameState.Selection)
            return false;

        runningPlayers = new List<PlayerStatus>(readyPlayers);

        return EnabledPlayers() == readyPlayers.Count;
    }

    public void ReportNewFloor(int newFloor)
    {
        if(highestFloor < newFloor)
        {
            highestFloor = newFloor;
            floor = newFloor;
            generator.SetHighestFloor(highestFloor);
            cameraPosition.SetHighestFloor(highestFloor);
        }
    }
}
