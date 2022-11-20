using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeCreepEnemy : ShooterEnemy
{
    public float laserCooldown = 5f;
    private bool shooting;
    private float timer;

    protected override void Start()
    {
        base.Start();
        timer = laserCooldown;
        shooting = false;
    }

    protected override void Update()
    {
        Debug.Log(timer);

        rb.mass = takingKnockback ? 2 : 1000000;

        if (gun == null)
            return;

        timer -= Time.deltaTime;

        if (!shooting)
        {
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

            if (shootingTarget != null && timer > 0f && timer <= 0.5f)
            {
                // rotate to target
                Vector3 rotation = target.transform.position - transform.position;
                // the -90 on the following line is due to sprite rotation reasons
                // w/ the pathfinding script
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);

                shooting = true;
            }
        }

        if (shooting)
        {
            aiPath.canMove = false;
            if (timer < 0)
                Attack();
        }
        else if (timer > 0)
        {
            // move closer
            if (timer < 0.5f)
                timer = 0.5f;

            aiPath.canMove = !takingKnockback;
            Move();
        } else if (timer < -0.5f)
        {
            timer = laserCooldown;
        }

        gun.UpdateTimer();
    }

    protected override void Attack()
    {
        base.Attack();
        shooting = false;
    }
}
