using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : PlayerShooter
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (moveDirection.x > 0)
            sprite.flipX = false;
        else if (moveDirection.x < 0)
            sprite.flipX = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
