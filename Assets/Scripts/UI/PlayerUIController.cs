using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    public Text _text;
	void Start () {
		
	}

    public void SetScoreText(string text)
    {
        _text.text = text;
    }
}
