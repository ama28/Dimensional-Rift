using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
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

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // we shot an enemy!
            enemy.TakeDamage(damage);
            enemy.TakeKnockback(rb.velocity * knockback);

            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }
    }
}
