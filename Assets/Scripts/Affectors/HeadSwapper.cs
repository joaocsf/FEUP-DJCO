using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwapper : MonoBehaviour {

    public Sprite[] heads;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStyle styler = other.GetComponent<PlayerStyle>();
        if(styler != null)
        {
            styler.SetHeadSprite(heads[Random.Range(0, heads.Length)]); 
        }
    }
}
