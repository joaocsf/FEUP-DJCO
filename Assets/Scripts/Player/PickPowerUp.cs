using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;
    private PowerUpGenerator generator;

    private void Start()
    {
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
            if (Input.GetButtonDown("Fire" + gameObject.GetComponent<Movement>().playerID))
            {
                powerUp.Activate();
            }
            powerUp.Update();
        }
    }
}
