using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HomingEnemy : Enemy
{
    protected AIPath aiPath;
    protected P1Controller target;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        aiPath = GetComponent<AIPath>();
        target = FindObjectOfType<P1Controller>();
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
