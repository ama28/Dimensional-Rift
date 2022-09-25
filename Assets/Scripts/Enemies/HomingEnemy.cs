using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HomingEnemy : Enemy
{
    protected AIPath aiPath;
    protected PlayerFarmer target;

    protected HitInfo meleeHit = new HitInfo() {
        damage = 1f, 
        knockbackScalar = 3f
    };


    // Start is called before the first frame update
    protected override void Start()
    {
        aiPath = GetComponent<AIPath>();
        target = FindObjectOfType<PlayerFarmer>();
        meleeHit.source = this;
        health = 1;
    }

    protected override void Update()
    {
        base.Update();

        Move();
    }

    protected override void Move()
    {
        base.Move();

        aiPath.destination = target.transform.position;
    }

    protected override void Attack()
    {

    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag(target.tag)) {
            target.TakeDamage(meleeHit);
        }
    }

}
