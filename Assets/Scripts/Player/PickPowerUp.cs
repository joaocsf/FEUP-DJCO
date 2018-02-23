using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up"))
        {
            Debug.Log("Picked it up");
            Destroy(other.gameObject);

            PowerUpGenerator generator = new PowerUpGenerator();

            powerUp = generator.GetPowerUp(gameObject);
        }
    }

    private void Update()
    {
        //Update power up
        if(powerUp != null) {
            if (Input.GetButtonDown("Fire1" + gameObject.GetComponent<Movement>().playerID))
            {
                powerUp.Activate();
            }
            powerUp.Update();

            if (powerUp.isOver)
            {
                powerUp = null;
            }
        }
    }
}
