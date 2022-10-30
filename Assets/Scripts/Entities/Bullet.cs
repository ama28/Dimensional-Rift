using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private HitInfo hitInfo; //damage etc
    public Being source; //should be set when fired

    [HideInInspector]
    public float speed;
    private float damage = 1f;
    private float knockback;
    private int pierce = 1;
    private float splashRange = 0.0f;
    private float range;
    private bool falloff = false;

    private Vector3 pos0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pos0 = transform.position;
        SetHitInfo();
    }

    protected void Update()
    {
        float distanceTraveled = Vector3.Magnitude(pos0 - transform.position);

        if (distanceTraveled > range)
            Destroy(gameObject);
    }

    public virtual void SetUpBullet(Being source, GunInfo gunInfo) {
        this.source = source;
        speed = gunInfo.bulletSpeed;
        damage = gunInfo.damage;
        knockback = gunInfo.knockback;
        pierce = gunInfo.pierce;
        splashRange = gunInfo.splashRange;
        range = gunInfo.range;
    }

    protected virtual void SetHitInfo() {
        if (falloff)
        {
            float dampFactor = 1 - (Vector3.Magnitude(pos0 -
                transform.position) / range);
            hitInfo.damage = damage * dampFactor;
        } else
            hitInfo.damage = damage;

        hitInfo.source = source;
        hitInfo.sourcePos = transform.position;
        hitInfo.knockbackScalar = knockback;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        Being being = other.GetComponent<Being>();

        // don't count collision if bullet hits owner
        if (being == source)
            return;

        if(other.tag == "PlayerFarmer"
            && source != null
            && source.tag == "PlayerShooter") {
            return;
        }

        if (other.tag == source.tag)
            return;

        if (other.layer == LayerMask.NameToLayer("Wall"))
        {
            // shot a wall :(
            Destroy(gameObject);
            return;
        }

        if (being != null)
        {
            // we shot an enemy!
            SetHitInfo();
            being.TakeDamage(hitInfo);

            //TODO: splash damage

            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }
    }
}
