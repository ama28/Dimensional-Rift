using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : PlayerShooter
{
    private new BoxCollider2D collider;
    private bool grounded = false;

    private Vector2 moveDirection;
    private Transform samSprite;

    private Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        collider = gameObject.transform.GetComponent<BoxCollider2D>();
        samSprite = gameObject.transform.GetChild(0).GetComponent<Transform>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", grounded);
        
        //flip sprite
        if (moveDirection.x > 0)
            samSprite.eulerAngles = new Vector3(samSprite.eulerAngles.x, 180, samSprite.eulerAngles.z);
        else if (moveDirection.x < 0)
            samSprite.eulerAngles = new Vector3(samSprite.eulerAngles.x, 0, samSprite.eulerAngles.z);
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
            StartCoroutine(jumpTrigger());
            rb.velocity = new Vector2(rb.velocity.x, stats.jumpForce);
        }
    }

    IEnumerator jumpTrigger()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetTrigger("Jump");
    }
}
