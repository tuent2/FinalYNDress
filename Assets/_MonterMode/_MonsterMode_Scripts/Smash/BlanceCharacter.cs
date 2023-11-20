using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlanceCharacter : MonoBehaviour
{
    public float targetRotation = 0f;
    public float force = 500f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime));
    }
}
