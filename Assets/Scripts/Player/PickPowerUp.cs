using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up"))
        {
            Debug.Log("Picked up");
            Destroy(other.gameObject);

            PowerUpGenerator generator = new PowerUpGenerator();

            powerUp = generator.GetPowerUp(gameObject);
        }
    }
}
