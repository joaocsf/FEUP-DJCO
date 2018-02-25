using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(Movement))]
public class PlayerUIManager : MonoBehaviour, IPlayerEvents
{
    PlayerStatus status;

    public void OnControllDisabled()
    {
        status.UIEnabled(false);
    }

    public void OnControllEnabled()
    {
        status.UIEnabled(true);
    }

    private void Start()
    {

        GetComponent<Movement>().AddPlayerEventListener(this);
        status = GetComponent<PlayerStatus>();
       

    }
}