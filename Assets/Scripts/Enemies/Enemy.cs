using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyProperties properties; //max health, damage, etc
    private int health;
    public int Health => health;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //basic enemy initialization

        
    }

    protected abstract void Attack();
    protected virtual void Move() {
        //pathfinding shiz, possibly do setup in here and then specific behaviors in derived class

    }

    protected virtual void Die() {
        Destroy(this);
    }

    public virtual void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

}
