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

    [SerializeField] private FarmStats stats;
    
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
    }

    protected virtual void OnActionPhaseStart(Wave wave) {
        spriteRenderers[0].sprite = growingSprite;
    }

    protected virtual void OnBuildPhaseStart() {
        Debug.Log("starting");
        roundsSinceHarvest++;
        if(roundsSinceHarvest >= stats.roundsToHarvest) {
            StartCoroutine(OnHarvest());
            roundsSinceHarvest = 0;
        }
    }

    protected IEnumerator OnHarvest() {
        spriteRenderers[0].sprite = grownSprite;
        yield return new WaitForSeconds(Random.Range(1, 2.5f));
        GameManager.Instance.currency += stats.coinsOnHarvest;
        //show small + (coin sprite) x coinsOnHarvest ?
        spriteRenderers[0].sprite = startSprite;
        yield return null;
    }
}
