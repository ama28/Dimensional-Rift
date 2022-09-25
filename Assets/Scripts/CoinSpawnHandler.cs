using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnHandler : MonoBehaviour
{

    public float ySpawnBound;
    public float xSpawnBound;
    public float spawnRate;
    public int maxCoins;
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
        if(currCoins < maxCoins){
            GameObject nc = Instantiate(newCoin, this.transform) as GameObject; // Spawn new coin 
            nc.transform.localPosition = new Vector3(Random.Range(-xSpawnBound, xSpawnBound), 
                                                    Random.Range(-ySpawnBound, ySpawnBound), 0);
                                                    // Change coin location to random position within 
                                                    // x and y spawn bounds of position of CoinSpawnHandler
            currCoins++;
        }

    }

}
