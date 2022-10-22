using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Controller : PlayerFarmer
{
    public Transform grabDetector;

    [SerializeField]
    private float rayDist;
    [SerializeField]
    private float rotationSpeed;

    private Vector2 moveDirection;

    public bool isHolding = false;
    private Transform heldObject;

    private Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        health = maxHealth;
        myHealth = FindObjectOfType<HealthBar>();
    }

    private void Update()
    {
        Debug.DrawRay(grabDetector.position, transform.up * rayDist, Color.green);

        if (moveDirection != Vector2.zero && !isHolding)
        {
            Quaternion rotationGoal = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * stats.speed, moveDirection.y * stats.speed);
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetector.position, Vector2.up * transform.localScale, rayDist);

        if (isHolding)
        {
            if (context.canceled)
            {
                heldObject.SetParent(null);
                isHolding = false;
                stats.speed += 2;
                heldObject = null;
                Debug.Log("object dropped");
            }
        }
        else if (!isHolding && grabCheck.collider != null && grabCheck.collider.tag == "Movable")
        {
            heldObject = grabCheck.transform;

            if (context.started)
            {
                isHolding = true;
                stats.speed -= 2;
                heldObject.SetParent(transform);
                Debug.Log("object picked up");
            }
        }
    }
}
