using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    public Text _text;
    public Image headSlot;
	void Start () {
		
	}

    public void SetScoreText(string text)
    {
        _text.text = text;
    }

    public void SetHeadSprite(Sprite s)
    {
        headSlot.sprite = s;
        
    }
}
