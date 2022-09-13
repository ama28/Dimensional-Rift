using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform grabDetector;

    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float rayDist;
    [SerializeField]
    private float rotationSpeed;

    private Vector2 moveDirection;

    public bool isHolding = false;
    private Transform heldObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
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
                heldObject.SetParent(transform);
                Debug.Log("object picked up");
            }
        }
    }
}
