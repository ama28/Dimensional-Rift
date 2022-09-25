using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmer : Player
{
    public class FarmerStats : PlayerStats {
        public float invulnerabilityTime = 1.0f;
    }

    public FarmerStats stats = new FarmerStats();

    public virtual void Start() {
        Debug.Log(stats);
        health = stats.maxHealth;
    }

    public void TakeDamage(HitInfo hit) {
        if(!invulnerable) {
            health -= hit.damage;
            //knockback
            Vector2 knockback = (transform.position - hit.source.transform.position).normalized;
            knockback *= hit.knockbackScalar * 1000;
            Debug.Log(knockback);
            rb.AddForce(knockback);
            StartCoroutine(Invulnerability());
        }
    }

    IEnumerator Invulnerability() {
        invulnerable = true;
        yield return new WaitForSeconds(stats.invulnerabilityTime);
        invulnerable = false;
    }


}
