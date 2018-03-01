using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwapper : MonoBehaviour {

    public Sprite[] Heads;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStyle styler = other.GetComponent<PlayerStyle>();
        if(styler != null)
        {
            styler.SetHeadSprite(Heads[Random.Range(0, Heads.Length)]); 
        }
    }
}
