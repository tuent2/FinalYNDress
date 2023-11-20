using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    Collider2D[] collider2ds;

    void Start()
    {
        IgnoreColliders();
    }
    public void IgnoreColliders()
    {
        if (collider2ds == null)
        {
            collider2ds = GetComponentsInChildren<Collider2D>();
        }
        for (int i = 0; i < collider2ds.Length; i++)
        {
            for (int j = i + 1; j < collider2ds.Length; j++)
            {
                Physics2D.IgnoreCollision(collider2ds[i], collider2ds[j]);
            }
        }
    }
}
