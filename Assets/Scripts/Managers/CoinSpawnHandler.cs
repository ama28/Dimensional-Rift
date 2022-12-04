using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnHandler : MonoBehaviour
{

    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;
    public int maxCoins;
    private float spawnRate;
    private int maxCoinTotal = 1;
    private int totalCoinsSpawned = 0;
    public float preventSpawnRadius = 1f;
    private int currCoins;
    public GameObject newCoin;

    void OnEnable() {
        GameManager.OnBuildPhaseStart += EndWave;
        GameManager.OnActionPhaseStart += StartSpawning;
    }

    void OnDisable() {
        GameManager.OnBuildPhaseStart -= EndWave;
        GameManager.OnActionPhaseStart -= StartSpawning;
    }

    void StartSpawning(Wave wave)
    {
        InvokeRepeating("SpawnCoin", 0f, spawnRate);
        currCoins = 0;
        totalCoinsSpawned = 0;
        maxCoinTotal = wave.maxCoinTotal;
        spawnRate = wave.coinSpawnTimer;
    }
    
    public void OnCollect(){ // This is shitty code design but it works
        AudioManager.Coin();
        currCoins--;
    }

    // Method for spawning coin
    public void SpawnCoin(){
        if(totalCoinsSpawned >= maxCoinTotal) { //all coins for round spawned
            return;
        }

        if(currCoins < maxCoins){
            Vector3 spawnLoc = new Vector3(Random.Range(xMin, xMax), 
                                        Random.Range(yMin, yMax), 0);

            int attempts = 0;
            while(!PreventSpawnOverlap(spawnLoc)){
                // Keep changing spawn location until valid spawn
                spawnLoc = new Vector3(Random.Range(xMin, xMax), 
                                        Random.Range(yMin, yMax), 0);
                if(PreventSpawnOverlap(spawnLoc)){
                    break;
                }
                if(attempts > 100) {
                    Debug.LogWarning("Coin placement attempts exceeded 100!");
                    break;
                }
            }

            GameObject nc = Instantiate(newCoin, this.transform) as GameObject; // Spawn new coin 
            nc.transform.position = spawnLoc;
            nc.transform.parent = this.transform;
            totalCoinsSpawned++;
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

    public void EndWave() {
        CancelInvoke("SpawnCoin");
        ClearCoins();
    }

    public void ClearCoins() {
        Coin[] coins = GetComponentsInChildren<Coin>();
        foreach(Coin coin in coins) {
            Destroy(coin.gameObject);
        }
    }
    
}