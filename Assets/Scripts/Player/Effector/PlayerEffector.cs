using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerEffector : MonoBehaviour {

    private Effector effector;
    private PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>(); 
    }

    public void SetEffector(Effector effector)
    {
        if (this.effector != null)
            this.effector.Remove();
        this.effector = effector;
        effector.Initialize(status);
    }

    public void Update()
    {
        if(effector != null)
            effector.DeltaUpdate(Time.deltaTime);
    }
}
