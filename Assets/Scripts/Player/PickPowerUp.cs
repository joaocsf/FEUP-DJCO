using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;
    private PowerUpGenerator generator;
    private PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        generator = GameObject.FindObjectOfType<PowerUpGenerator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up"))
        {
            Debug.Log("Picked it up");
            Destroy(other.gameObject);

            powerUp = generator.GetPowerUp(gameObject);
        }
    }

    private void Update()
    {
        //Update power up
        if(powerUp != null) {
            if (status.Input.Fire())
            {
                powerUp.Activate();
            }
            powerUp.OnUpdate(Time.deltaTime);
        }
    }
}
