using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    [Header("General")]
    public bool debug = false;
    public float floorHeight = 1f;
    public float floorWidth = 1f;
    public int tilesNumber = 5;

    [Header("Generation")]
    public int initialFloors = 6;
    public int generateFloorsAbove = 1;
    //public int deleteFloorsBelow = 1;
    [Range(0, 100)]
    public int powerUpChance = 5;
    [Range(0, 100)]
    public int elevatorChance = 50;
    [Range(0, 100)]
    public int brokenChance = 20;
    public int stairsMinimum = 1;
    public int stairsMaximum = 2;

    [Header("Walls Objects")]
    public GameObject wall;
    public GameObject backWall;
    public GameObject backSolidWall;

    [Header("Floor Objects")]
    public GameObject backgroundFloor;
    public GameObject normalFloor;
    public GameObject brokenFloor;
    public GameObject stairsFloor;

    [Header("Background Objects")]
    public GameObject background;
    public GameObject elevator;
    public GameObject[] backgroundDecoration;

    [Header("Background")]
    public Color dayColor = Color.blue;
    public int dayColorFloor = 0;
    public Color duskColor = Color.red;
    public int duskColorFloor = 10;
    public Color nightColor = Color.black;
    public int nightColorFloor = 20;

    private enum TileType { Default, Broken, Stairs, Elevator, PowerUp, BrokenPowerUp }

    private int currentFloor = 0;
    
    private Gradient gradient = new Gradient();
    private float gradientEnd = 1f;

    private List<GameObject> floors = new List<GameObject>();
    private List<TileType[]> floorTiles = new List<TileType[]>();
    private int[] tilesBlocked;

    private GameObject[] players;
    private int highestFloor = 0;

    void Start () {
        //Initiate Players
        players = GameObject.FindGameObjectsWithTag("Player");

        //Initiate Background Gradient
        GradientColorKey[] colorKeys;
        GradientAlphaKey[] alphaKeys;
        gradientEnd *= nightColorFloor;

        colorKeys = new GradientColorKey[3];
        colorKeys[0].color = dayColor;
        colorKeys[0].time = dayColorFloor / gradientEnd;
        colorKeys[1].color = duskColor;
        colorKeys[1].time = duskColorFloor / gradientEnd;
        colorKeys[2].color = nightColor;
        colorKeys[2].time = nightColorFloor / gradientEnd;
        alphaKeys = new GradientAlphaKey[0];
        /*alphaKeys[0].alpha = 1.0F;
        alphaKeys[0].time = 0.0F;
        alphaKeys[1].alpha = 1.0F;
        alphaKeys[1].time = 1.0F;*/
        gradient.SetKeys(colorKeys, alphaKeys);

        Debug.Log(gradient);

        //Initiate Floors
        tilesBlocked = new int[tilesNumber];
        for(int i = 0; i < initialFloors; i++)
        {
            AddFloor();
        }
    }

    // Update is called once per frame
    void Update () {
        foreach (var player in players)
        {
            int floor = Mathf.FloorToInt(player.transform.position.y / floorHeight);
            highestFloor = Mathf.Max(highestFloor, floor);
        }

        if (currentFloor - highestFloor <= generateFloorsAbove)
        {
            AddFloor();
            DeleteFloor();
            if(debug)
                Debug.Log("New Highest Floor: " + highestFloor);
        }

    }

    private void AddFloor()
    {
        TileType[] tiles = GenerateFloor();
        InstantiateFloor(tiles);
        currentFloor++;
    }

    private void DeleteFloor()
    {
        floorTiles.RemoveAt(0);
        Destroy(floors[0]);
        floors.RemoveAt(0);
    }


    private GameObject SpawnObject(GameObject prefab, Transform parent, int xPosition)
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);

        obj.transform.localPosition = new Vector3(xPosition * floorWidth, 0, 0);
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = new Vector3(floorWidth, 1, 1);

        return obj;
    }
   

    /**
     * Instantiate a floor
     **/
    private void InstantiateFloor(TileType[] tiles)
    {
        //Instantiate floor object
        GameObject floor = new GameObject("Floor" + currentFloor);
        floor.transform.parent = transform;
        floor.transform.localEulerAngles = new Vector3(-90, 0, 0);
        floor.transform.localPosition = new Vector3(0, currentFloor * floorHeight, 0);
        floors.Add(floor);

        //Get Tiles

        //Intantiate Components
        InstantiatePlayableFloor(floor.transform, tiles);
        InstantiateDecoration(floor.transform, tiles);
    }

    /**
    * Instantiate the playable floor
    **/
    private void InstantiatePlayableFloor(Transform parent, TileType[] tiles)
    {
        for (int i = 0; i < tilesNumber; i++)
        {
            GameObject prefab;
            switch (tiles[i])
            {
                case TileType.Broken:
                case TileType.BrokenPowerUp:
                    prefab = brokenFloor;
                    break;
                case TileType.Stairs:
                    prefab = stairsFloor;
                    break;
                default:
                    prefab = normalFloor;
                    break;

            }
            SpawnObject(prefab, parent, i);
        }  
    }

    /**
     * Instantiate the decorations
     **/
    private void InstantiateDecoration(Transform parent, TileType[] tiles)
    {
        GameObject obj;

        //Instantiate right wall
        obj = SpawnObject(wall, parent, 0);
        obj.transform.localScale = new Vector3(-1, 1, 1);
        obj.transform.localPosition = new Vector3(-floorWidth / 2, 0, 0);

        //Instantiate left wall
        obj = SpawnObject(wall, parent, tilesNumber);
        obj.transform.localPosition = new Vector3(tilesNumber * floorWidth - floorWidth / 2, 0, 0);

        //Instantiate background floor
        obj = SpawnObject(backgroundFloor, parent, 0);
        obj.transform.localPosition = new Vector3((tilesNumber * floorWidth) / 2f - floorWidth / 2, 0f, 0f);
        obj.transform.localScale = new Vector3((tilesNumber) * floorWidth, 1, 1);

        //Instantiate background
        obj = SpawnObject(background, parent, 0);
        obj.GetComponent<Renderer>().materials[0].color = generateColor();
        obj.transform.localPosition = new Vector3((tilesNumber * floorWidth) / 2f - floorWidth / 2, 3f, floorHeight / 2);
        obj.transform.localScale = new Vector3((tilesNumber) * floorWidth, floorHeight, 1);
        obj.transform.localEulerAngles = new Vector3(-90f, 0, 0);


        for (int i = 0; i < tilesNumber; i++)
        {
            //Instantiate background wall
            obj = SpawnObject((i % 2 == 0) ? backWall : backSolidWall, parent, i);
          

            GameObject prefab;
            switch (tiles[i])
            {
                case TileType.Elevator:
                    prefab = elevator;
                    break;
                default:
                    prefab = backgroundDecoration[Random.Range(0, backgroundDecoration.Length)];
                    break;

            }
            if(prefab != null)
                SpawnObject(prefab, parent, i);

            
        }//*/
    }

    private Color generateColor()
    {
         return gradient.Evaluate(currentFloor / gradientEnd);
    }


    /**
     * Generates a new floor
     **/
    private TileType[] GenerateFloor()
    {
        //Generate default tiles
        TileType[] tiles = new TileType[tilesNumber];
        floorTiles.Add(tiles);
        if (currentFloor != 0)
        {
            GenerateStairs(tiles);
            GenerateElevators(tiles);
            GenerateBrokenFloor(tiles);
            GeneratePowerUps(tiles);
        }
        //Unblock the tiles
        for (int i = 0; i < tilesNumber; i++)
        {
            tilesBlocked[i] = Mathf.Max(tilesBlocked[i] - 1, 0);
        }

        if (debug)
        {
            string str = "[";
            foreach (var t in tiles)
            {
                str += t;
                str += ";";
            }
            str += "]";
            Debug.Log(str);
        }
        return tiles;
    }

    /**
     * Generates the stairs
     **/
    private void GenerateStairs(TileType[] tiles)
    {
        int stairsNumber = Random.Range(stairsMinimum, stairsMaximum+1);
        while(stairsNumber > 0)
        {
            int tile = Random.Range(0, tilesNumber);
            if (tilesBlocked[tile] == 0 && tiles[tile] == TileType.Default)
            {
                tiles[tile] = TileType.Stairs;
                tilesBlocked[tile] = 2;
                stairsNumber--;
            }
        }
    }

    /**
     * Generates the elevator
     **/
    private void GenerateElevators(TileType[] tiles)
    {
        bool genElevator = Random.Range(0, 100) < elevatorChance;
        if (genElevator)
        {
            int tile = Random.Range(0, tilesNumber);
            if (tilesBlocked[tile] == 0 && tiles[tile] == TileType.Default)
            {
                tiles[tile] = TileType.Elevator;
                tilesBlocked[tile] = 3;
            }
        }
    }
    
    /**
     * Generates the powerUps
     **/
    private void GenerateBrokenFloor(TileType[] tiles)
    {
        for (int tile = 0; tile < tilesNumber; tile++)
        {
            if (tilesBlocked[tile] == 0 && Random.Range(0, 100) < brokenChance)
            {
                tiles[tile] = TileType.Broken;
            }
        }
    }

    /**
     * Generates the powerUps
     **/
    private void GeneratePowerUps(TileType[] tiles)
    {
        for (int tile = 0; tile < tilesNumber; tile++)
        {
            if (tilesBlocked[tile] == 0 && Random.Range(0, 100) < powerUpChance)
            {
                if (tiles[tile] == TileType.Broken)
                    tiles[tile] = TileType.BrokenPowerUp;
                else if (tiles[tile] == TileType.Default)
                    tiles[tile] = TileType.PowerUp;
            }
        }
    }
}
