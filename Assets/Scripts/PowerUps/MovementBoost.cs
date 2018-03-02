using UnityEngine;

[CreateAssetMenu(fileName = "MovementBoost", menuName = "PowerUps/MovementBoost", order = 1)]
public class MovementBoost : PowerUp
{
    public float speed;
    public float jumpSpeed;
    public float effectTime;
    private float elapsedTime = 0;
    private PlayerStyle style;

    protected override bool OnActivate()
    {
        Movement m = player.GetComponent<Movement>();
        style = player.GetComponent<PlayerStyle>();

        m.speed = speed;
        m.jumpSpeed = jumpSpeed;

        style.SetPowerUpSprite(null);
        style.SetFeetSprite(sprite);

        return true;
    }

    protected override void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
        //Debug.Log("Got here " + elapsedTime);
        if (elapsedTime >= effectTime)
        {
            Movement m = player.GetComponent<Movement>();
            m.ResetSpeed();
            m.ResetJumpSpeed();

            style.ResetFeetSprite();
            Destroy(this);
        }
    }
}
