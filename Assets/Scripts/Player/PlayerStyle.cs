using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerStyle : MonoBehaviour {

    public Color mainColor;

    private PlayerStatus status;
    public SpriteRenderer body;
    public SpriteRenderer head;
    public SpriteRenderer powerUp;
    public SpriteRenderer rightFoot, leftFoot;

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

    public void SetPowerUpSprite(Sprite s)
    {
        powerUp.sprite = s;
    }

    public void SetFeetSprite(Sprite s)
    {
        rightFoot.sprite = s;
        leftFoot.sprite = s;
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
