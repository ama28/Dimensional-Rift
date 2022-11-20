using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HomingEnemy : Enemy
{
    protected AIPath aiPath;
    protected PlayerFarmer target;

    protected bool takingKnockback;
    
    [SerializeField]
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
        meleeHit.sourcePos = transform.position;
        takingKnockback = false;
    }

    protected override void Update()
    {
        base.Update();

        Move();
    }

    protected override void Move()
    {
        base.Move();

        Debug.Log(target);
        aiPath.destination = target.transform.position;
    }

    protected override void Attack()
    {

    }

    // damaging player (melee)
    protected void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.CompareTag(target.tag)
            && meleeHit.damage > 0) {
            target.TakeDamage(meleeHit);
        }
    }

    public override void TakeDamage(HitInfo hit)
    {
        base.TakeDamage(hit);
        //knockback
        Vector3 angle = transform.position - hit.sourcePos;
        TakeKnockback((new Vector2(angle.x, angle.y)).normalized * hit.knockbackScalar);
    }

    // KB script, temporarily disables pathfinding
    public override void TakeKnockback(Vector2 kb)
    {
        takingKnockback = true;
        aiPath.canMove = false;
        base.TakeKnockback(kb);
        StopCoroutine(RecoverFromKnockback(kb));
        StartCoroutine(RecoverFromKnockback(kb));
    }

    // works with TakeKnockback(kb) to reenable pathfinding
    // after a bit
    protected virtual IEnumerator RecoverFromKnockback(Vector2 kb)
    {
        yield return new WaitForSeconds(0.1f);

        float t = 0;
        while (t < 1f) //for the first second,
        {
            //or until speed in the direciton of the knockback! is below 0.7f,
            if ((Vector2.Dot(rb.velocity, kb.normalized) * rb.velocity.magnitude) < 1f) { t = 1f; } 
            rb.AddForce(-400f * Time.deltaTime * rb.velocity); //resist the movement
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        takingKnockback = false;
        aiPath.canMove = true;
    }
}
