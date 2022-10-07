using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : PlayerShooter
{
    private SpriteRenderer sprite;
    private new BoxCollider2D collider;
    private bool grounded = false;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        collider = gameObject.transform.GetComponent<BoxCollider2D>();
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
        rb.velocity = new Vector2(moveDirection.x * stats.speed, rb.velocity.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, (collider.size.y / 2) - 0.3f, LayerMask.GetMask("Wall"));
        grounded = hit.collider;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, stats.jumpForce);
        }
    }
}
