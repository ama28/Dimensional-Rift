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

        public List<EnemyInfo> enemies;

        public int GetTotalEnemyCount() {
            int total = 0;
            enemies.ForEach(x => total += x.count);
            return total;
        }

        public Enemy ChooseEnemy() {
            return enemies[0].enemy;
        }


    }
