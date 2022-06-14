using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public float speed;
    public float pHorizontal;
    public float pVertical;
    public float horizontal;
    public float vertical;

    private Vector2 movement;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement.x = horizontal;
        movement.y = vertical;

        speed = movement.magnitude;
        if(speed > 0.01)
        {
            pHorizontal = horizontal;
            pVertical = vertical;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
    }
}
