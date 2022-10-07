using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Wave {

        [System.Serializable]
        public class EnemyInfo {
            public Enemy enemy;
            public int count;
        }

        [Header("Enemy Info")]
        public List<EnemyInfo> enemies;

        public float enemySpawnDelay = 0.5f;
        [Header("Coin Info")]
        public int maxCoinTotal = 1;
        public float coinSpawnTimer = 3.0f;

        public int GetTotalEnemyCount() {
            int total = 0;
            enemies.ForEach(x => total += x.count);
            return total;
        }

        public Enemy ChooseEnemy() {
            return enemies[0].enemy; //TODO: RANDOMIZE ENEMIES
        }


    }
