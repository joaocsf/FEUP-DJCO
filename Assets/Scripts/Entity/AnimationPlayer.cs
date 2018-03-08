using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour {

    public int animation = 0;

	void Start () {
        GetComponent<Animator>().SetInteger("Animation", animation);		
	}
}
