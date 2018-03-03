using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "Spawner", menuName = "PowerUps/Spawner", order = 1)]
public class Spawner : PowerUp
{
    public GameObject prefab;
    public int ammo = 1;
    protected override bool OnActivate()
    {
        ammo--;
        GameObject.Instantiate(prefab, player.transform.position, Quaternion.identity);
        if(ammo <= 0)
            Destroy(this);

        return ammo <= 0;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }
}
