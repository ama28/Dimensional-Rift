using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    [System.Serializable]
    public class EnemyInfo {
        public Enemy enemy;
        public int cost = 1;
        public float weight = 1;
    }
    public Vector2Int waveRange = new Vector2Int(1, 1);

    [Header("Enemy Info")]
    public List<EnemyInfo> enemies;

    public int baseEnemyBudget = 1;
    public int extraEnemyBudget;
    public float enemySpawnDelay = 0.5f;
    [Header("Coin Info")]
    public int maxCoinTotal = 1;
    public float coinSpawnTimer = 3.0f;
    
    public List<Enemy> GetEnemyList() {
        List<Enemy> enemyList = new List<Enemy>();
        int budget = baseEnemyBudget; /*+ (
            GameManager.Instance.spawnManager.currentWave - waveRange.x) * extraEnemyBudget; */

        float maxWeightRoll = 0;
        enemies.ForEach(x => maxWeightRoll += x.weight);

        while (budget > 0)
        {
            float roll = Random.Range(0f, maxWeightRoll);
            int ebCheck = budget;

            float rollTracker = 0;
            foreach (EnemyInfo enemyInfo in enemies)
            {
                rollTracker += enemyInfo.weight;
                if (roll <= rollTracker)
                {
                    budget -= enemyInfo.cost;
                    enemyList.Add(enemyInfo.enemy);
                    break;
                }
            }

            if (budget == ebCheck)
            {
                Debug.LogError("Wave calculations somehow got goofed");
                return enemyList;
            }
        }

        return enemyList;
    }
}
