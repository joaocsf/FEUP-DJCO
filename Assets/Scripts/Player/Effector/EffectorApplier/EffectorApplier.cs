using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectorApplier : MonoBehaviour
{
    public Effector effector;
    public bool destroyOnOthers = false;
    public bool destroyOnEffect = false;
    public float delayTime = 0;

    public float autoDestroy = 10f;

    private bool destroyed = false;
    private List<IEffectorApplierEvents> listeners = new List<IEffectorApplierEvents>();

    IEnumerator Start()
    {
        listeners.AddRange(GetComponents<IEffectorApplierEvents>());
        if (delayTime > 0f)
            GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(autoDestroy);
        destroy();
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

    public void destroy(){
        if(destroyed)
            return;

        destroyed = true;

        listeners.ForEach((o) => o.OnPickup());

        if (destroyOnEffect)
        {
            bool canDestoy = AlertListenersDestroy();
            if(canDestoy)
                Destroy(gameObject);
        }
 
    }

    public void OnCollision(GameObject other)
    {
        
        PlayerEffector effector = other != null? other.GetComponent<PlayerEffector>() : null;
        if (effector == null && !destroyOnOthers)
            return;

        if(effector != null)
            effector.SetEffector(Instantiate(this.effector) as Effector);

        destroy();
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
