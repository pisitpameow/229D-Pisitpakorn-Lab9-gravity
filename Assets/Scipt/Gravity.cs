using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> otherObjectslist;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectslist == null)
        {
            otherObjectslist = new List<Gravity>();
        }

        otherObjectslist.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectslist)
        {
            if (obj != this)
            {
                AttractionForce(obj);
            }
        }
    }

    void AttractionForce(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 dir = rb.position - otherRb.position;
        float dist = dir.magnitude;
        if (dist == 0)
        {
            return;
        }

        float forceMagitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(dist, 2));
        Vector3 gravitionalForce = forceMagitude * dir.normalized;
        otherRb.AddForce(gravitionalForce);
    }

}
