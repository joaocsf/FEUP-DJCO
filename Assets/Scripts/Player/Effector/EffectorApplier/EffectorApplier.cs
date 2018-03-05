using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectorApplier : MonoBehaviour
{
    public Effector effector;
    public bool destroyOnOthers = false;
    public bool destroyOnEffect = false;
    public float delayTime = 0;

    private List<IEffectorApplierEvents> listeners = new List<IEffectorApplierEvents>();

    void Start()
    {
        listeners.AddRange(GetComponents<IEffectorApplierEvents>());
        if (delayTime > 0f)
            GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (delayTime < 0) return;
        delayTime-= Time.deltaTime;
        if (delayTime < 0)
            GetComponent<Collider>().enabled = true;
    }

    public bool AlertListenersDestroy()
    {
        bool res = true;
        listeners.ForEach((lstnr) =>
        {
            if (!lstnr.OnDelete())
                res = false;
        });

        return res;
    }

    public void OnCollision(GameObject other)
    {
        PlayerEffector effector = other.GetComponent<PlayerEffector>();
        if (effector == null && !destroyOnOthers)
            return;

        if(effector != null)
            effector.SetEffector(Instantiate(this.effector) as Effector);

        listeners.ForEach((o) => o.OnPickup());

        if (destroyOnEffect)
        {
            bool canDestoy = AlertListenersDestroy();
            if(canDestoy)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision.gameObject);
    }
}
