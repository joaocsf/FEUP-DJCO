using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    private List<Transform> transforms = new List<Transform>();

    public float smoothRatio = 2f;
    public Vector3 compensation = Vector3.zero;

    private Vector3 targetPos = Vector3.zero;

    private int highestFloor = 0;

	void Start () {
	    	
	}
    
    public void AddTransform(Transform t)
    {
        transforms.Add(t);
    }

    public void SetHighestFloor(int floor)
    {
        highestFloor = floor;
    }


    void FixedUpdate () {

        Vector3 center = Vector3.zero;

        center.x = GameController.floorWidth * GameController.tilesNumber / -2;
        center.y = highestFloor * GameController.floorHeight;
      
        targetPos = center + compensation;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * smoothRatio);

	}
}
