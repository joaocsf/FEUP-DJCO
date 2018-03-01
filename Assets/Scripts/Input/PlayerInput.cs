using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput {

    public int ID { get; private set; }
    private string left, right, jump, fire;

    public PlayerInput(int playerID)
    {
        ID = playerID;
    }
    
    public void ParseInput(string str)
    {
        string[] keys = str.Split(' ');
        Debug.Log(str + " " + str.Length);
        if (keys.Length != 4)
            return;
        left = keys[0];
        right = keys[1];
        jump = keys[2];
        fire = keys[3];
        Debug.Log(left + " " + right + " " + jump + " " + fire);
    }

    public bool Jump()
    {
        return Input.GetButtonDown("Jump" + ID) || Input.GetKeyDown(jump);
    }

    public bool Fire()
    {
        return Input.GetButtonDown("Fire" + ID) || Input.GetKeyDown(fire);
    }

    public float Horizontal()
    {
        float input = Input.GetAxisRaw("Horizontal" + ID);
        Debug.Log("Here");
        if(input == 0f)
        {
            Debug.Log("Here");
            input = 0;
            if (Input.GetKey(left))
                input += -1f;
            if(Input.GetKey(right))
                input += 1f;
        }
        return input;
    }
}
