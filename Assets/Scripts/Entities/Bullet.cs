using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public HitInfo hitInfo; //damage etc
    public Being source; //should be set when fired

    // fancy parameter things
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float knockback;
    [SerializeField]
    private int pierce = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        Vector3 rotation = transform.position - mousePos;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);

        SetHitInfo();
    }

    protected virtual void SetHitInfo() {
        hitInfo.damage = damage;
        hitInfo.source = source;
        hitInfo.sourceTransform = transform;
        hitInfo.knockbackScalar = knockback;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.layer == LayerMask.NameToLayer("Wall"))
        {
            // shot a wall :/
            Destroy(gameObject);
            return;
        }

        Being being = other.GetComponent<Being>();
        if (being != null)
        {
            // we shot an enemy!
            SetHitInfo();
            being.TakeDamage(hitInfo);

            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }
    }
}
