using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static string defaultInputs = "a d w e\n[4] [6] [8] [9]\nleft right up [0]\ng j y u";
    private static PlayerInput[] players;

    static InputManager()
    {
        players = new PlayerInput[4];
        int index = 1;
        foreach(string s in defaultInputs.Split('\n'))
        {
            PlayerInput pi = new PlayerInput(index);
            pi.ParseInput(s);
            players[index - 1] = pi;
            index++;
        }
    }

    public static PlayerInput GetInput(int index)
    {
        if(index > 0 && index < 5)
            return players[index-1];
        return null;
    }

    

}
