using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus),typeof(PlayerStyle))]
public class PickPowerUp : MonoBehaviour {

    public PowerUp powerUp;
    private PowerUpGenerator generator;
    private PlayerStatus status;
    private PlayerStyle style;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        style = GetComponent<PlayerStyle>();
        generator = GameObject.FindObjectOfType<PowerUpGenerator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power Up"))
        {
            if(powerUp != null)
            {
                powerUp.OnDeactivate();
            }
            powerUp = generator.GetPowerUp(gameObject);

            style.SetPowerUpSprite(powerUp.sprite);
        }
        else if(other.gameObject.GetComponent<SinglePowerUpGenerator>() != null)
        {
            SinglePowerUpGenerator gen = other.gameObject.GetComponent<SinglePowerUpGenerator>();
            if (powerUp != null)
            {
                powerUp.OnDeactivate();
            }
            powerUp = gen.GetPowerUp(gameObject);
            Destroy(other.gameObject);
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
            powerUp.UpdateNow(Time.deltaTime);
        }
    }
}
