using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : PlayerShooter
{
    private new BoxCollider2D collider;
    private bool grounded = false;
    private bool moving = false;
    // public float maxSpeed = 8.0f;
    private float groundedMultiplier;
    private Transform parent;
    private Vector3 previous;

    private Camera mainCam;

    private Vector2 moveDirection;
    private Transform samSprite;

    private Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        collider = gameObject.transform.GetComponent<BoxCollider2D>();
        samSprite = gameObject.transform.GetChild(0).GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 mouseWorldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        if(Mathf.Abs(rb.velocity.x) > 0.9f) {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x * 0.25f));
        } else {
            animator.SetFloat("Speed", 0);
        }
        animator.SetBool("isGrounded", grounded);
        
        if(parent) {
            transform.position = transform.position + (parent.position - previous) * 0.87f;

            previous = parent.position;
        }
        
    }

    private void LateUpdate() {
        
    }

    private void FixedUpdate()
    {
        // rb.velocity = new Vector2(moveDirection.x * stats.speed, rb.velocity.y);
        if(moveDirection == Vector2.zero) {
            // if(rb.velocity.magnitude > 1) {

            // } else {
            //     rb.AddRelativeForce(rb.velocity.x)
            // }
            rb.AddRelativeForce(new Vector2(rb.velocity.x * -20f * Mathf.Pow(groundedMultiplier, 3), 0));
        } else if(Mathf.Abs(rb.velocity.x) < stats.speed * 3) {
            if(moveDirection.x * rb.velocity.x < 0) {
                rb.AddRelativeForce(new Vector2(moveDirection.x * stats.speed * 50 * groundedMultiplier, 0));
            } else {
                rb.AddRelativeForce(new Vector2(moveDirection.x * stats.speed * 40 * groundedMultiplier, 0));
            }
        }

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
                // transform.SetParent(hit.transform);
                parent = hit.transform;
                previous = parent.position;
                groundedMultiplier = 1.0f;
            } else {
                // transform.SetParent(null);
                parent = null;
                previous = Vector3.zero;
                groundedMultiplier = stats.airMovementPenalty;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(context.performed) {
            if(Mathf.Abs(context.ReadValue<Vector2>().x - moveDirection.x) > 1) {
                //turn-around force
                rb.AddForce(new Vector2(moveDirection.x * stats.turnAroundScalar * groundedMultiplier, 0));
            }
        }
        moveDirection = context.ReadValue<Vector2>();
        Debug.Log(moveDirection);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            StartCoroutine(jumpTrigger());
            rb.AddForce(new Vector2(0, stats.jumpForce * 100));
        }
    }

    IEnumerator jumpTrigger()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetTrigger("Jump");
    }
}
