using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // preliminary spawn manager based off guides such as 
    // https://answers.unity.com/questions/1754348/how-do-i-add-multiple-different-enemies-to-a-wave.html
    // https://frankgwarman.medium.com/using-coroutines-in-unity-and-c-creating-a-spawn-manager-442a7b6096cd
    

    private int levelCounter = 1;
    // values that can be changed depending on what game designers want
    private int maxSpawns = 1;
    // incrememnt whenever we spawn an enemy
    private int spawnCounter = 0;
    // boolean to detect when all enemies are dead
    private bool isDead = false;
    // choke points for where to spawn enemies
    public Transform[] spawnPoints;

    // list of enemies
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        // not all enemies are dead
        if(!isDead) {
            return;
        }
        else {
            levelCounter += 1;
            StartCoroutine(SpawnLoop());
        }
    }

     IEnumerator SpawnLoop()
     {
        isDead = false;
        maxSpawns = 4 * levelCounter + 5;
        for(int i = 0; i < maxSpawns; i++) {
            SpawnEnemy(i % 4);
            spawnCounter += 1;
            yield return new WaitForSeconds(2);
        }
        yield break;
     }
 
     void SpawnEnemy(int enemy_index)
     {
         Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
         Instantiate(enemies[enemy_index], _sp.position, _sp.rotation); 
     }
 }
