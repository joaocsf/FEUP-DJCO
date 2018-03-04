using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "Spawner", menuName = "PowerUps/Spawner", order = 1)]
public class Spawner : PowerUp
{
    public GameObject prefab;
    public float placementDelta;
    public int ammo = 1;

    protected override bool OnActivate()
    {
        ammo--;

        Vector3 position = player.transform.position;
        position.x -= placementDelta * player.transform.localScale.x;

        GameObject.Instantiate(prefab, position, Quaternion.identity);
        if (ammo <= 0)
            OnDeactivate(); 

        return ammo <= 0;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }
}
