using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public float floorHeight = 1f;
    public float floorWidth = 1f;

    public int tileNumber = 5;

    public GameObject backgroundFloor;
    public GameObject wall;
    public GameObject backWall;
    public GameObject[] backgroundDecoration;

    public GameObject[] floorObjects;

    private int currentFloor = 0;

    private List<GameObject> floors = new List<GameObject>();

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateFloor();
	}

    private GameObject SpawnObject(GameObject prefab, Transform parent, int xPosition)
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);

        obj.transform.localPosition = new Vector3(xPosition * floorWidth, 0, 0);
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = new Vector3(floorWidth, 1, 1);

        return obj;
    }

    private void GeneratePlayableFloor(Transform parent)
    {
        for (int i = 0; i < tileNumber; i++)
            SpawnObject(floorObjects[Random.Range(0, floorObjects.Length)], parent, i);
        
    }

    private void GenerateDecoration(Transform parent) {

        GameObject obj = SpawnObject(wall, parent, 0);
        obj.transform.localScale = new Vector3(-1, 1, floorHeight);
        obj.transform.localPosition = new Vector3(-floorWidth / 2, 0, 0);


        obj = SpawnObject(wall, parent, tileNumber);
        obj.transform.localPosition = new Vector3(tileNumber * floorWidth - floorWidth / 2,0,0);
        obj.transform.localScale = new Vector3(1, 1, floorHeight);

        obj = SpawnObject(backgroundFloor, parent, 0);
        obj.transform.localPosition = new Vector3((tileNumber * floorWidth) / 2f - floorWidth/2, 0f, 0f);
        obj.transform.localScale = new Vector3((tileNumber) * floorWidth, 1, 1);

        for (int i = 0; i < tileNumber; i++)
        {
            SpawnObject(backgroundDecoration[Random.Range(0, backgroundDecoration.Length)], parent, i);
            obj = SpawnObject(backWall, parent, i);
            obj.transform.localScale = new Vector3(1, 1, floorHeight);
        }
    }

    private void GenerateFloor()
    {
        GameObject floor = new GameObject("Floor" + currentFloor);
        floor.transform.parent = transform;
        floor.transform.localEulerAngles = new Vector3(-90, 0, 0);
        floor.transform.localPosition = new Vector3(0, currentFloor * floorHeight, 0);
        floors.Add(floor);

        GeneratePlayableFloor(floor.transform);
        GenerateDecoration(floor.transform);

        currentFloor++;
    }
}
