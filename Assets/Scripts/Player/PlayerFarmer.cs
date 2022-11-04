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
    public SpriteRenderer spriteRenderer;

    public virtual void Start() {
        
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
            if(health <= 0) {
                GameManager.Instance.SetGameState(GameManager.GameStateType.GameOver);
            }
        }
    }

    IEnumerator Invulnerability() {
        invulnerable = true;

        float timeElapsed = 0;
        float blinkTime = 0.15f;
        float blinkCoefficient = 0.85f;
        while(timeElapsed < stats.invulnerabilityTime) {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            blinkTime = Mathf.Max(blinkTime * blinkCoefficient, 2 * Time.deltaTime);
            timeElapsed += blinkTime;
            Debug.Log(timeElapsed);
            yield return new WaitForSeconds(blinkTime);
        }
        spriteRenderer.enabled = true;
        invulnerable = false;
    }

}
