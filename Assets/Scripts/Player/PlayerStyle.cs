using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerStyle : MonoBehaviour {

    public Color mainColor;

    private PlayerStatus status;
    public SpriteRenderer body;
    public SpriteRenderer head;

	void Start () {
        status = GetComponent<PlayerStatus>();
        body.color = mainColor;
	}
	
	void Update () {
		
	}

    public void SetHeadSprite(Sprite s)
    {
        head.sprite = s;
        status.SetHeadSprite(s);
    }

    public void UpdateSortingLayer(Transform transform, int id)
    {
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = id;
        }

        foreach (Transform t in transform)
            UpdateSortingLayer(t, id);
    }

}
