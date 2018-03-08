using UnityEngine;
using System.Collections;

public class DisplacementController : MonoBehaviour
{

    Material mat;

    public float scaleRate;
    public float maxScale = 200;
    public float distrotion = 0.5f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        transform.localScale += Time.deltaTime * scaleRate * Vector3.one;
        float f = 1f - Mathf.Clamp(transform.localScale.x/maxScale, 0f, 1f);
        mat.SetFloat("_Transparency", f*distrotion);

    }
}
