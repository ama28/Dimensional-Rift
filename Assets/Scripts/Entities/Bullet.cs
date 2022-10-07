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
    public float speed;
    public float range = 0f;
    [SerializeField]
    private float damage = 1f;
    [SerializeField]
    private float knockback;
    [SerializeField]
    private int pierce = 1;
    [SerializeField]
    private float splashRange = 0.0f; 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetHitInfo();
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
        hitInfo.damage = damage;
        hitInfo.source = source;
        hitInfo.sourcePos = transform.position;
        hitInfo.knockbackScalar = knockback;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        Being being = other.GetComponent<Being>();

        if(being == GameManager.Instance.playerFarmer 
                && source == GameManager.Instance.playerShooter) {
            return;
        }

        if (other.layer == LayerMask.NameToLayer("Wall"))
        {
            // shot a wall :/
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
