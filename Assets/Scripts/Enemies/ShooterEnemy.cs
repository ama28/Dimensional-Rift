using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : HomingEnemy
{
    public Gun gun;
    public float targetRange;
    public float moveUntilRange;

    private Being[] shootingTargets;
    private Being shootingTarget;

    protected override void Start()
    {
        base.Start();

        if (gun != null)
        {
            gun.owner = this;
            shootingTargets = FindObjectsOfType<PlayerFarmer>();
        }
    }

    protected override void Update()
    {
        if (gun == null)
            return;

        // get closest player
        shootingTarget = null;
        float targetDistance = targetRange;
        foreach (Being b in shootingTargets)
        {
            float bDistance = Vector3.Magnitude(b.transform.position -
                transform.position);
            if (bDistance < targetDistance)
            {
                shootingTarget = b;
                targetDistance = bDistance;
            }
        }

        if (shootingTarget != null)
        {
            // rotate to target
            Vector3 rotation = target.transform.position - transform.position;
            // the -90 on the following line is due to sprite rotation reasons
            // w/ the pathfinding script
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            Attack();
        }

        if (shootingTarget == null ||
            Vector3.Magnitude(shootingTarget.transform.position -
                transform.position) > moveUntilRange)
        {
            // too far away, we should move closer
            aiPath.canMove = true;
            Move();
        } else
        {
            aiPath.canMove = false;
        }

        gun.UpdateTimer();
    }

    protected override void Attack()
    {
        gun.Fire();
    }
}
