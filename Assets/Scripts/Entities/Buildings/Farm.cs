using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    [System.Serializable]
    public class FarmStats {
        public int roundsToHarvest = 1;
        public float maxHealth = 50;
        public int coinsOnHarvest = 5;
    }

    public FarmStats stats;
    public FarmHealthBar farmHealthBar;
    
    [Header("Sprites")]
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private Sprite grownSprite;
    
    private int roundsSinceHarvest = 0;

    protected virtual void OnEnable() {
        GameManager.OnActionPhaseStart += OnActionPhaseStart;
        GameManager.OnBuildPhaseStart += OnBuildPhaseStart;
    }

    protected virtual void OnDisable() {
        GameManager.OnActionPhaseStart -= OnActionPhaseStart;
        GameManager.OnBuildPhaseStart -= OnBuildPhaseStart;
    }

    protected override void Start()
    {
        base.Start();
        health = stats.maxHealth;
        farmHealthBar.gameObject.SetActive(false);
    }

    protected virtual void OnActionPhaseStart(Wave wave) {
        spriteRenderers[0].sprite = growingSprite;
    }

    protected virtual void OnBuildPhaseStart() {
        roundsSinceHarvest++;
        health += Mathf.Clamp(stats.maxHealth/5, 0, stats.maxHealth);
        if(health == stats.maxHealth) { //hide health bar if full health
            farmHealthBar.gameObject.SetActive(false);
        } else {
            farmHealthBar.UpdateHealthBar();
        }
        if(roundsSinceHarvest >= stats.roundsToHarvest) {
            spriteRenderers[0].sprite = grownSprite;
        }
    }

    public override void OnPlace()
    {
        base.OnPlace();
        GameManager.Instance.BuildingManager.targetableBuildings.Add(this);
    }

    protected virtual void Die() {
        GameManager.Instance.BuildingManager.RemoveBuilding(this);
        // GameManager.Instance.BuildingManager.targetableBuildings.Remove(this);
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public override void TakeDamage(HitInfo hit) {
        base.TakeDamage(hit);
        if(!farmHealthBar.gameObject.activeInHierarchy) {
            farmHealthBar.gameObject.SetActive(true);
        }
        farmHealthBar.UpdateHealthBar();
        if(health <= 0) {
            Die();
        }
    }

    protected IEnumerator OnHarvestAnim() {
        //show small + (coin sprite) x coinsOnHarvest ?
        spriteRenderers[0].sprite = startSprite;
        yield return null;
    }

    public virtual void OnHarvest() {
        if(roundsSinceHarvest >= stats.roundsToHarvest) {
            roundsSinceHarvest = 0;
            GameManager.Instance.currency += stats.coinsOnHarvest;
            StartCoroutine(OnHarvestAnim());
        }
    }

}
