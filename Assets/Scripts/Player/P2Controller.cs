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

        { //grounded check
            Vector3 offset = new Vector3(collider.offset.x, collider.offset.y, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, (collider.size.y / 2) + 0.1f, LayerMask.GetMask("Wall"));
            grounded = hit.collider;
            //checking edges of collider
            if(!grounded) {
                offset = new Vector3(collider.offset.x + collider.size.x/2, collider.offset.y, 0);
                hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, (collider.size.y / 2) + 0.1f, LayerMask.GetMask("Wall"));
                grounded = hit.collider;
            }
            if(!grounded) {
                offset = new Vector3(collider.offset.x - collider.size.x/2, collider.offset.y, 0);
                hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, (collider.size.y / 2) + 0.1f, LayerMask.GetMask("Wall"));
                grounded = hit.collider;
            }
            
            Debug.Log(grounded);
            if(grounded) {
                transform.SetParent(hit.transform);
            } else {
                transform.SetParent(null);
            }
        }
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
