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
        base.Start();

        aiPath = GetComponent<AIPath>();
        target = FindObjectOfType<PlayerFarmer>();
        meleeHit.source = this;
        meleeHit.sourceTransform = transform;
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

    // damaging player (melee)
    protected void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag(target.tag)) {
            target.TakeDamage(meleeHit);
        }
    }

    public override void TakeDamage(HitInfo hit)
    {
        base.TakeDamage(hit);
        if(hit.sourceTransform) {
            Vector3 angle = transform.position - hit.sourceTransform.position;
            TakeKnockback((new Vector2(angle.x, angle.y)).normalized * hit.knockbackScalar);
        }
    }

    // KB script, temporarily disables pathfinding
    public override void TakeKnockback(Vector2 kb)
    {
        base.TakeKnockback(kb);

        if (kb.x > 1 && kb.y > 1)
        {
            aiPath.canMove = false;
            StopCoroutine(RecoverFromKnockback());
            StartCoroutine(RecoverFromKnockback());
        }
    }

    // works with TakeKnockback(kb) to reenable pathfinding
    // after a bit
    protected IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(0.1f);

        float t = 0;
        while (t < 1f)
        {
            if (rb.velocity.x < 1f && rb.velocity.y < 1f) { t = 1f; }
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        aiPath.canMove = true;
    }
}
