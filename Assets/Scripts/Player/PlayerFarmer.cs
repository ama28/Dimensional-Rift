using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmer : Player
{
    public HealthBar myHealth;

    [System.Serializable]
    public class FarmerStats : PlayerStats {
        public float invulnerabilityTime = 1.0f;
    }

    public FarmerStats stats = new FarmerStats();

    public virtual void Start() {
        Debug.Log(stats);
    }

    public override void TakeDamage(HitInfo hit) {
        base.TakeDamage(hit);
        if(!invulnerable) {
            //knockback
            Vector2 knockback = (transform.position - hit.sourcePos).normalized;
            knockback *= hit.knockbackScalar * 1000;
            Debug.Log(knockback);
            rb.AddForce(knockback);
            StartCoroutine(Invulnerability());

            myHealth.UpdateHealthBar();
            if(health < 0) {
                GameManager.Instance.SetGameState(GameManager.GameStateType.GameOver);
            }
        }
    }

    IEnumerator Invulnerability() {
        invulnerable = true;
        //TODO: blink sprite while invulnerable
        yield return new WaitForSeconds(stats.invulnerabilityTime);
        invulnerable = false;
    }


}
