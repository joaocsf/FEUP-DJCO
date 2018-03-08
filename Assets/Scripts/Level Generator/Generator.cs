using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{

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
    public int brokenChance = 0;
    public int brokenChanceGrowth = 1;
    public int stairsMinimum = 1;
    public int stairsMaximum = 2;
    public int blockRadius = 1;

    [Header("Walls Objects")]
    public GameObject wall;
    public GameObject backWall;
    public GameObject backSolidWall;

    [Header("Floor Objects")]
    public GameObject backgroundFloor;
    public GameObject normalFloor;
    public GameObject brokenFloor;
    public GameObject stairsFloor;
    public GameObject powerUp;

    [Header("Background Objects")]
    public GameObject background;
    public GameObject elevator;
    public GameObject[] backgroundDecoration;

    [Header("Background")]
    public float scale = 3f;
    public Color dayColor = Color.blue;
    public int dayColorFloor = 0;
    public Color duskColor = Color.red;
    public int duskColorFloor = 10;
    public Color nightColor = Color.black;
    public int nightColorFloor = 20;

    public GameObject thing;
    public GameObject floorThing;
    public float spawnDelta = 1f;
    public int thingsPerFloor = 2;

    private WaitForSeconds spawnDeltaSeconds;

    private enum TileType { Default, Broken, Stairs, Elevator, PowerUp, BrokenPowerUp }

    private int currentFloor = 0;

    private Gradient gradient = new Gradient();
    private float gradientEnd = 1f;

    private List<GameObject> floors = new List<GameObject>();
    private List<TileType[]> floorTiles = new List<TileType[]>();
    private enum BlockType { None, Stairs, StairsSide, StairsUp, Elevator, ElevatorTunnel, ElevatorExit, ElevatorExitStairsSide}
    private BlockType[] tilesBlocked;

    private GameObject[] players;
    private int highestFloor = 0;

    void Start()
    {
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

        spawnDeltaSeconds = new WaitForSeconds(spawnDelta);
        StartCoroutine(SpawnThings());

        tilesBlocked = new BlockType[tilesNumber];
        for (int i = 0; i < initialFloors; i++)
        {
            AddFloor();
        }
    }

    public void SetHighestFloor(int floor)
    {
        highestFloor = floor;
    }



    // Update is called once per frame
    void Update()
    {
        if (currentFloor - highestFloor <= generateFloorsAbove)
        {
            AddFloor();
            DeleteFloor();
            SpawnThingsEachFloor();
            if (debug)
                Debug.Log("New Highest Floor: " + highestFloor);
        }

    }

    private IEnumerator SpawnThings()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelta);
            if (thing != null)
            {
                GameObject obj = GameObject.Instantiate(thing);
                obj.transform.localPosition = new Vector3(Random.Range(0, floorWidth * tilesNumber) * -1, currentFloor * floorHeight, -0.5f);
                Debug.Log("Spawn Thing");
            }
        } 
    }

    private void SpawnThingsEachFloor()
    {
        if (floorThing == null)
            return;
        for (int i = 0; i < thingsPerFloor; i++)
        {
            GameObject obj = GameObject.Instantiate(floorThing);
            obj.transform.localPosition = new Vector3(Random.Range(0, floorWidth * tilesNumber) * -1, currentFloor * floorHeight, -0.5f);
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
                    prefab = brokenFloor;
                    break;
                case TileType.BrokenPowerUp:
                    SpawnObject(powerUp, parent, i);
                    prefab = brokenFloor;
                    break;
                case TileType.Stairs:
                    prefab = stairsFloor;
                    break;
                case TileType.PowerUp:
                    SpawnObject(powerUp, parent, i);
                    prefab = normalFloor;
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
        obj.transform.localScale = new Vector3((tilesNumber) * floorWidth * scale, floorHeight, 1);
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
            if (prefab != null)
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
        //PrintBlock("Floor " + currentFloor + " before gen");
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
        //PrintBlock("Floor " + currentFloor + " after gen");
        UnblockTiles();
        //PrintBlock("Floor " + currentFloor + " after unblock");
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

    private void PrintBlock(string head)
    {
        string str = head + ":[";
        foreach (var t in tilesBlocked)
        {
            str += t;
            str += ";";
        }
        str += "]";
        Debug.Log(str);
    }

    /**
    * Blocks Tiles
    * Made for Stairs 
    **/
    private void UnblockTiles()
    {
        for (int i = 0; i < tilesNumber; i++)
        {
            switch (tilesBlocked[i])
            {
                case BlockType.Stairs:
                case BlockType.StairsSide:
                case BlockType.ElevatorExitStairsSide:
                    tilesBlocked[i] = BlockType.StairsUp;
                    break;
                case BlockType.Elevator:
                    tilesBlocked[i] = BlockType.ElevatorTunnel;
                    break;
                case BlockType.ElevatorTunnel:
                    tilesBlocked[i] = BlockType.ElevatorExit;
                    break;
                default:
                    tilesBlocked[i] = BlockType.None;
                    break;
            }
        }
    }


    /**
     * Returns the number of free tiles
     **/
    private int GetFreeTiles()
    {
        int res = 0;
        for (int i = 0; i < tilesBlocked.Length; i++)
        {
            if (tilesBlocked[i] == BlockType.None)
                res++;
        }
        return res;
    }


    /**
    * Blocks Tiles
    * Made for Stairs 
    **/
    private void BlockTiles(int tile, int height, int radius)
    {
        for (int i = (tile - radius); i <= (tile + radius); i++)
        {
            if (i >= 0 && i < tilesBlocked.Length && tilesBlocked[i] != BlockType.ElevatorTunnel) {
                if (tilesBlocked[i] == BlockType.ElevatorExit)
                    tilesBlocked[i] = BlockType.ElevatorExitStairsSide;
                else
                    tilesBlocked[i] = BlockType.StairsSide;
                }
        }
        tilesBlocked[tile] = BlockType.Stairs;
    }

    /**
     * Generates the stairs
     **/
    private void GenerateStairs(TileType[] tiles)
    {
        int stairsNumber = Random.Range(stairsMinimum, stairsMaximum + 1);
        while (stairsNumber > 0)
        {

            stairsNumber = Mathf.Min(stairsNumber, GetFreeTiles());
            int tile = Random.Range(0, tilesNumber);
            if (tilesBlocked[tile] == BlockType.None && tiles[tile] == TileType.Default)
            {
                tiles[tile] = TileType.Stairs;
                BlockTiles(tile, 2, blockRadius);
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
            if (tilesBlocked[tile] == BlockType.None && tiles[tile] == TileType.Default)
            {
                tiles[tile] = TileType.Elevator;
                tilesBlocked[tile] = BlockType.Elevator;
            }
        }
    }

    /**
     * Generates the powerUps
     **/
    private void GenerateBrokenFloor(TileType[] tiles)
    {
        float change = brokenChance + brokenChanceGrowth * currentFloor;
        for (int tile = 0; tile < tilesNumber; tile++)
        {
            if (tilesBlocked[tile] != BlockType.Elevator && tilesBlocked[tile] != BlockType.ElevatorExit
                && tilesBlocked[tile] != BlockType.ElevatorExitStairsSide
                && tilesBlocked[tile] != BlockType.Stairs && Random.Range(0, 100) < change)
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
            if (/*tilesBlocked[tile] <= 0 &&*/ Random.Range(0, 100) < powerUpChance)
            {
                if (tiles[tile] == TileType.Broken)
                    tiles[tile] = TileType.BrokenPowerUp;
                else if (tiles[tile] == TileType.Default)
                    tiles[tile] = TileType.PowerUp;
            }
        }
    }
}
