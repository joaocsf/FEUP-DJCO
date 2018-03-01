using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerUIManager : MonoBehaviour, IPlayerEvents
{
    PlayerStatus status;

    public void OnDeActivated()
    {
        status.UIEnabled(false);
    }

    public void OnActivated()
    {
        status.UIEnabled(true);
    }

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        status.AddPlayerEventListener(this);
    }
}