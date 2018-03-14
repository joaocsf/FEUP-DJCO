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

    public float AlertListenersDestroy()
    {
        float res = 0;
        listeners.ForEach((lstnr) =>
        {
            res = Mathf.Max(res, lstnr.OnDelete());
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
            float timeDestroy = AlertListenersDestroy();
            Destroy(gameObject, timeDestroy);
        }
 
    }

    public void OnCollision(GameObject other)
    {
        
        PlayerEffector effector = other != null? other.GetComponent<PlayerEffector>() : null;
        EffectorApplier applyer = other != null? other.GetComponent<EffectorApplier>() : null;
        if (effector == null && applyer == null && !destroyOnOthers)
            return;

        if(effector != null)
            effector.SetEffector(Instantiate(this.effector) as Effector);

        if (applyer != null)
            applyer.destroy();

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
