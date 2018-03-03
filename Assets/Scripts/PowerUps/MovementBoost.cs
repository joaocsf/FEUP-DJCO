using UnityEngine;

[CreateAssetMenu(fileName = "MovementBoost", menuName = "PowerUps/MovementBoost", order = 1)]
public class MovementBoost : PowerUp
{
    public float speed;
    public float jumpSpeed;
    public float effectTime;
    private float elapsedTime = 0;
    private Movement movement;

    public override void Initialize(GameObject player)
    {
        base.Initialize(player);

        movement = player.GetComponent<Movement>();
    }

    protected override bool OnActivate()
    {
        movement.speed = speed;
        movement.jumpSpeed = jumpSpeed;

        style.SetFeetSprite(sprite);
        return true;
    }

    protected override void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
        if (elapsedTime >= effectTime)
        {
            OnDeactivate();
        }
    }

    public override void OnDeactivate()
    {
        movement.ResetSpeed();
        movement.ResetJumpSpeed();

        style.ResetFeetSprite();

        base.OnDeactivate();
    }
}
