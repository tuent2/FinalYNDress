using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRb2d : MonoBehaviour
{
    Rigidbody2D rb2d;
    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D playerCollider = GetComponent<Collider2D>();


            if (playerCollider.OverlapPoint(mousePosition))
            {

                isDragging = true;
                //offset = transform.position - hit.point;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newPosition;
        }
    }
}
