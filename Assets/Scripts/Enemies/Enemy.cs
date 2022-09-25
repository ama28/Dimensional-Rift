using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health = 1;

    public int id = -1; //id, should be set on initialization

    // Start is called before the first frame update
    protected virtual void Start()
    {

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
        Destroy(this);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
