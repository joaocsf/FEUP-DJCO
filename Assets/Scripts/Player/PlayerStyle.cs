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

}
