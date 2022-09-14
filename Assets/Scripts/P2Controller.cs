using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float jumpForce = 4f;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
