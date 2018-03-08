using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerStyle : MonoBehaviour {

    public Color mainColor;
    public Sprite defaultFeet;

    private PlayerStatus status;
    private Animator anim;
    public SpriteRenderer body;
    public SpriteRenderer head;
    public SpriteRenderer powerUp;
    public SpriteRenderer rightFoot, leftFoot;

    void Start () {
        status = GetComponent<PlayerStatus>();
        body.color = mainColor;
        anim = GetComponent<Animator>();
	}

    public void From(PlayerStyle style)
    {
        SetHeadSprite(style.GetHeadSprite());
        body.color = style.body.color;
    }

    public Sprite GetHeadSprite()
    {
        return head.sprite;
    }

    public void SetHeadSprite(Sprite s)
    {
        head.sprite = s;
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

    public void SetConfused(bool state)
    {
        anim.SetBool("Confused", state);
    }

    public void ResetFeetSprite()
    {
        rightFoot.sprite = defaultFeet;
        leftFoot.sprite = defaultFeet;
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
