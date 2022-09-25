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
    // boolean to detect when all enemies are dead
    //private bool isDead = false;
    // choke points for where to spawn enemies
    public Transform[] spawnPoints;

    public Wave currentWave;

    // list of instantiated enemies;
    private List<Enemy> instanced;

    // Start is called before the first frame update
    void Awake() {
        instanced = new List<Enemy>();
    }
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    //Start wave when action phase starts
    void OnEnable() {
        GameManager.OnActionPhaseStart += StartWave;
    }

    void OnDisable() {
        GameManager.OnActionPhaseStart -= StartWave;
    }

    public void StartWave(Wave wave) {
        currentWave = wave;
        spawnCounter = 0;
        StartCoroutine(SpawnLoop());
    }

    //is called from enemy function on death
    public void RemoveEnemy(int id) {
        instanced.RemoveAll(x => x.id == id);
        // all enemies are dead
        if(instanced.Count == 0) {
            GameManager.Instance.SetGameState(GameManager.GameStateType.BuildPhase);
        }
    }

    IEnumerator SpawnLoop()
    {
        // spawn enemies one at a time up to max spawns enemies every 0.5 seconds
        // currentWave.maxSpawns = GameManager.Instance.Level + 1; //this will be set in the Wave Object
        for(int i = 0; i < currentWave.GetTotalEnemyCount(); i++) {
            SpawnEnemy(currentWave.ChooseEnemy());
            spawnCounter += 1;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    // picks a random spawnpoint to spawn enemy
    // unsure if we need to adapt it to work for more than a small amount of spawn points
     void SpawnEnemy(Enemy enemy)
     {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy newEnemy = Instantiate(enemy, _sp.position, _sp.rotation);
        newEnemy.id = spawnCounter; //give enemies an id so when they die we can remove more easily
        instanced.Add(newEnemy); 
     }
 }
