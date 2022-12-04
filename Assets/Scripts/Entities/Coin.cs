using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Coin : Collectible
{
    private CoinSpawnHandler coinSpawnHandler;

    void Start() {
        coinSpawnHandler = GameObject.Find("CoinSpawner").GetComponent<CoinSpawnHandler>();
    }
    public override void OnTriggerEnter2D(Collider2D c){
        // If player collects coin
        if(c.gameObject.tag == "PlayerFarmer"){
            //coinSpawnHandler.SpawnCoin(); // Spawn new coin
            coinSpawnHandler.OnCollect(); // Decrement coin
            AudioManager.Instance.Coin(0);
            GameManager.Instance.currency++;
            base.OnTriggerEnter2D(c); //destroys coin
        }

        
    }
}
