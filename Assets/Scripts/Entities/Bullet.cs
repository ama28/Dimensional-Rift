using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public HitInfo hitInfo; //damage etc
    public Being source; //should be set when fired

    // fancy parameter things
    [Tooltip("How fast the bullet moves.")]
    public float speed;
    [Tooltip("How far the bullet can travel before it despawns.")]
    public float range = 0f;
    [SerializeField]
    [Tooltip("How much damage the bullet will deal. If damageDamping " +
        "is enabled, this is the point-blank damage.")]
    private float damage = 1f;
    [SerializeField]
    [Tooltip("How much knockback the target will take when hit. " +
        "Set to 0 to disable knockback.")]
    private float knockback;
    [SerializeField]
    [Tooltip("The number of enemies the bullet can travel through " +
        "before despawning.")]
    private int pierce = 1;
    [SerializeField]
    private float splashRange = 0.0f;
    [SerializeField]
    [Tooltip("Wether or not to reduce bullet damage proportional " +
        "to lifetime remaining.")]
    private bool damageDamping = false;

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
        this.speed = gunInfo.bulletSpeed;
        this.damage = gunInfo.damage;
        this.knockback = gunInfo.knockback;
        this.pierce = gunInfo.pierce;
        splashRange = gunInfo.splashRange;
        range = gunInfo.range;
    }

    protected virtual void SetHitInfo() {
        if (damageDamping)
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
                && source.tag == "PlayerShooter") {
            return;
        }

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
