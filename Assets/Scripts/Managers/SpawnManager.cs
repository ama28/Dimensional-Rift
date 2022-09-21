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
    public Capsule[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    // checks to see if all enemies are dead
    // needs a isDead() script for the enemy game object
    void checkDead() {
        isDead = true;
        foreach(Capsule enemy in enemies) {
            isDead = isDead && enemy.isDead();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // all enemies are dead
        if(isDead) {
            spawnCounter = 0;
            levelCounter += 1;
            StartCoroutine(SpawnLoop());
        }
    }

     IEnumerator SpawnLoop()
     {
        // spawn enemies one at a time up to max spawns enemies every 0.5 seconds
        isDead = false;
        maxSpawns = levelCounter + 1;
        for(int i = 0; i < maxSpawns; i++) {
            SpawnEnemy(i % 4);
            spawnCounter += 1;
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(5);
        // check if dead every two seconds
        // add in once we actually have enemies instead of ovals

        while(!isDead) {
            checkDead();
            yield return new WaitForSeconds(2);
        }
     }
    
    // picks a random spawnpoint to spawn enemy
    // unsure if we need to adapt it to work for more than a small amount of spawn points
     void SpawnEnemy(int enemy_index)
     {
         Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
         Instantiate(enemies[enemy_index], _sp.position, _sp.rotation); 
     }
 }
