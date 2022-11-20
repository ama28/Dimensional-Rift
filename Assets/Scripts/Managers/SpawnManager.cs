using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // preliminary spawn manager based off guides such as 
    // https://answers.unity.com/questions/1754348/how-do-i-add-multiple-different-enemies-to-a-wave.html
    // https://frankgwarman.medium.com/using-coroutines-in-unity-and-c-creating-a-spawn-manager-442a7b6096cd

    //level counter has been moved to game manager

    // incrememnt whenever we spawn an enemy
    private int spawnCounter = 0;
    public int killCounter = 0;
    // boolean to detect when all enemies are dead
    //private bool isDead = false;
    // choke points for where to spawn enemies
    public Transform[] spawnPoints;

    public Wave currentWave;
    public int waveSize;

    public GameObject spaceCoinPrefab;

    // list of instantiated enemies;
    public List<Enemy> instanced;

    // Start is called before the first frame update
    void Awake() {
        instanced = new List<Enemy>();
    }

    //Start wave when action phase starts
    void OnEnable() {
        GameManager.OnActionPhaseStart += StartWave;
        GameManager.OnRestart += OnRestart;
    }

    void OnDisable() {
        GameManager.OnActionPhaseStart -= StartWave;
        GameManager.OnRestart -= OnRestart;
    }

    void Update() {
    }

    public void StartWave(Wave wave) {
        if(instanced.Count > 0) {
            Debug.LogError("Wave started with enemies still spawned!");
            instanced.ForEach(x => x.TakeDamage(new HitInfo() {damage = 1000})); //could cause issues if ppl don't do proper null checks
            instanced.Clear();
        }
        currentWave = wave;
        spawnCounter = 0;
        killCounter = 0;
        StartCoroutine(SpawnLoop());
    }

    //is called from enemy function on death
    public void RemoveEnemy(int id) {
        instanced.RemoveAll(x => x.id == id);
        killCounter++;
        // all enemies are dead
        if(instanced.Count == 0 && spawnCounter >= waveSize) {
            GameManager.Instance.SetGameState(GameManager.GameStateType.BuildPhase);
        }
    }

    // Spawns coin on enemy death
    public void spawnSpaceCoin(Transform t){
        GameObject sp = Instantiate(spaceCoinPrefab);
        sp.transform.position = t.position;
    }

    IEnumerator SpawnLoop()
    {
        // get the list of enemies from the wave
        List<Enemy> enemiesToSpawn = currentWave.GetEnemyList();
        waveSize = enemiesToSpawn.Count;
        Debug.Log("Starting new wave with size " + waveSize + "!");

        // spawn enemies from the list
        foreach (Enemy enemy in enemiesToSpawn) {
            SpawnEnemy(enemy);
            spawnCounter++;
            yield return new WaitForSeconds(currentWave.enemySpawnDelay);
        }
    }
    
    // picks a random spawnpoint to spawn enemy
    // unsure if we need to adapt it to work for more than a small amount of spawn points
     void SpawnEnemy(Enemy enemy)
     {
        Debug.Log("spawn " + spawnCounter);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy newEnemy = Instantiate(enemy, _sp.position, _sp.rotation);
        newEnemy.id = spawnCounter; //give enemies an id so when they die we can remove more easily
        instanced.Add(newEnemy); 
     }

    void OnRestart() {
        instanced.Clear();
    }
 }
