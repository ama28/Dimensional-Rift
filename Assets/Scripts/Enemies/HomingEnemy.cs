using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HomingEnemy : Enemy
{
    public float targetingDistance = 10f; //distance below which enemy will attack player instead of farm
    protected AIPath aiPath;
    
    [SerializeField]
    protected HitInfo meleeHit = new HitInfo() {
        damage = 1f, 
        knockbackScalar = 3f
    };

    public float cooldown = 1.5f;
    private float cd_time = 0; //store time since last cooldown

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        aiPath = GetComponent<AIPath>();
        SetTarget();
        meleeHit.source = this;
        meleeHit.sourcePos = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        Move();
    }

    protected override void Move()
    {
        base.Move();

        if(target != null) {
            aiPath.destination = target.transform.position;
        }

        SetTarget();
    }

    protected override void Attack()
    {

    }

    // damaging player (melee)
    protected void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject == target.gameObject) {
            target.TakeDamage(meleeHit);
                Debug.Log("DAMAGING 2");
        }
    }

    //damaging farms
    protected void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject == target.gameObject) {
            if(cd_time > cooldown)
            {
                target.TakeDamage(meleeHit);
                Debug.Log("DAMAGING");
                cd_time = 0;
            } else {
                cd_time += Time.deltaTime;
            }
        }
    }

    public override void TakeDamage(HitInfo hit)
    {
        base.TakeDamage(hit);
        //knockback
        Vector3 angle = transform.position - hit.sourcePos;
        TakeKnockback((new Vector2(angle.x, angle.y)).normalized * hit.knockbackScalar);
    }

    protected virtual void SetTarget() {
        Vector2 distance = GameManager.Instance.playerFarmer.transform.position - transform.position;
        if(distance.magnitude < targetingDistance || GameManager.Instance.BuildingManager.targetableBuildings.Count == 0) { //attack player if within range
            target = GameManager.Instance.playerFarmer;
        } else if (target == GameManager.Instance.playerFarmer || target == null) {
            //find closest building to target instead
            target = GameManager.Instance.BuildingManager.targetableBuildings.Find(x => {
                distance = x.transform.position - transform.position;
                //yes the nested lambda is gross but im tired and it works
                return GameManager.Instance.BuildingManager.targetableBuildings.TrueForAll(y => {
                    Vector2 distance2 = y.transform.position - transform.position;
                    return distance.magnitude <= distance2.magnitude;
                    });
            });
        }   
    }

    // KB script, temporarily disables pathfinding
    public override void TakeKnockback(Vector2 kb)
    {
        //if (kb.x > 1 && kb.y > 1) //unsure what this was for @liam?
        aiPath.canMove = false;
        base.TakeKnockback(kb);
        StopCoroutine(RecoverFromKnockback(kb));
        StartCoroutine(RecoverFromKnockback(kb));
    }

    // works with TakeKnockback(kb) to reenable pathfinding
    // after a bit
    protected IEnumerator RecoverFromKnockback(Vector2 kb)
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

        aiPath.canMove = true;
    }
}
