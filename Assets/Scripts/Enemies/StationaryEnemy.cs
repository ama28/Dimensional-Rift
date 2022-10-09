using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Enemy
{
    public Gun gun;
    public float range;

    private Being[] targets;
    private Being target;

    protected override void Start()
    {
        base.Start();

        if (gun != null)
        {
            gun.owner = this;
            targets = FindObjectsOfType<PlayerFarmer>();
        }
    }

    protected override void Update()
    {
        if (gun == null)
            return;

        // get closest player
        target = null;
        float targetDistance = range;
        foreach (Being b in targets)
        {
            float bDistance = Vector3.Magnitude(b.transform.position -
                transform.position);
            if (bDistance < targetDistance)
            {
                target = b;
                targetDistance = bDistance;
            }
        }

        if (target != null)
        {
            // rotate to target
            Vector3 rotation = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            Attack();
        }

        gun.UpdateTimer();
    }

    protected override void Attack()
    {
        gun.Fire();
    }
}
