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

    }

    protected virtual void OnBuildPhaseStart() {
        roundsSinceHarvest++;
        if(roundsSinceHarvest < stats.roundsToHarvest) {
            OnHarvest();
        }
    }

    protected virtual void OnHarvest() {
        GameManager.Instance.currency += stats.coinsOnHarvest;
    }

    protected IEnumerator HarvestAnim() {
        //show small + (coin sprite) x coinsOnHarvest ?
        yield return null;
    }
}
