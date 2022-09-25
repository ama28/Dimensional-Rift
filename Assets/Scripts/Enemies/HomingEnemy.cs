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

}
