using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Being
{

    [HideInInspector] public int id = -1; //id, should be set on initialization

    protected Rigidbody2D rb;
    private float initTime;
    protected SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        initTime = Time.timeSinceLevelLoad;
    }

    protected virtual void Update()
    {
        
    }

    protected abstract void Attack();
    protected virtual void Move()
    {
        //pathfinding shiz, possibly do setup in here and then specific behaviors in derived class

    }

    protected virtual void Die()
    {
        GameManager.Instance.spawnManager.RemoveEnemy(id);
        GameManager.Instance.spawnManager.spawnSpaceCoin(transform);
        Destroy(gameObject);
    }

    public override void TakeDamage(HitInfo hit)
    {
        base.TakeDamage(hit);
        AudioManager.Instance.EnemyDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void TakeKnockback(Vector2 kb)
    {
        rb.AddForce(kb * 50);
        spriteRenderer.color = Color.red;
    }
}
