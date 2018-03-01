using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;
    private PowerUpGenerator generator;
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
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
            if (movement.playerInput.Fire())
            {
                powerUp.Activate();
            }
            powerUp.OnUpdate(Time.deltaTime);
        }
    }
}
