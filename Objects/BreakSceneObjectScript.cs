using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSceneObjectScript : MonoBehaviour
{
    public GameObject breakedItem;

    public void Break()
    {
        GameObject breaked = Instantiate(breakedItem, transform.position, transform.rotation);
        Rigidbody[] rbs = breaked.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(1, transform.position, 30);
        }
        Destroy(gameObject);
    }
}
