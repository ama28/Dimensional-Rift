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

    private Interactable heldObject;

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

        if (moveDirection != Vector2.zero && heldObject == null)
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
        Debug.Log("wasd");
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("space");
        int layerMask = 1 << LayerMask.NameToLayer("Trigger");
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetector.position, Vector2.up * transform.localScale, rayDist, layerMask);

        if(grabCheck && grabCheck.transform.GetComponent<Interactable>() != null) {
            heldObject = grabCheck.transform.GetComponent<Interactable>();
        }

        if(heldObject != null) {
            if (context.canceled)
            {
                heldObject.OnRelease(this);

                stats.speed += 2;
                heldObject = null;
                animator.SetBool("Push", false);
                Debug.Log("object dropped");
            } else if (context.started)
            {
                heldObject.OnInteract(this);

                stats.speed -= 2;
                animator.SetBool("Push", true);
                Debug.Log("object picked up");
            }
        }
    }
}
