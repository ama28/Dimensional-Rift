using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnHandler : MonoBehaviour
{

    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;
    public float spawnRate;
    public int maxCoins;
    public float preventSpawnRadius = 1f;
    private int currCoins;
    public GameObject newCoin;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, spawnRate);
        currCoins = 0;
    }
    
    public void DecrementCoinCount(){ // This is shitty code design but it works
        currCoins--;
    }

    // Method for spawning coin
    public void SpawnCoin(){

        Vector3 spawnLoc = new Vector3(Random.Range(xMin, xMax), 
                                    Random.Range(yMin, yMax), 0);

        while(!PreventSpawnOverlap(spawnLoc)){
            // Keep changing spawn location until valid spawn
            spawnLoc = new Vector3(Random.Range(xMin, xMax), 
                                    Random.Range(yMin, yMax), 0);
            if(PreventSpawnOverlap(spawnLoc)){
                break;
            }
        }

        if(currCoins < maxCoins){
            GameObject nc = Instantiate(newCoin, this.transform) as GameObject; // Spawn new coin 
            nc.transform.position = spawnLoc;
            currCoins++;
        }
    }


    private bool PreventSpawnOverlap(Vector3 spawnPosition){
        LayerMask m = LayerMask.GetMask("Wall");
        Collider2D collider = Physics2D.OverlapCircle(spawnPosition, preventSpawnRadius, m);
        // Debug.Log(collider);
        if(collider == null){
            return true;
        }
        return false;
    }
    
}