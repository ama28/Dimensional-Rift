using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Coin : Collectible
{

    public override void OnTriggerEnter2D(Collider2D c){
        // If player collects coin
        if(c.gameObject.tag == "Player"){
            GameObject.Find("CoinSpawner").GetComponent<CoinSpawnHandler>().SpawnCoin(); // Spawn new coin
            GameObject.Find("CoinSpawner").GetComponent<CoinSpawnHandler>().DecrementCoinCount(); // Decrement coin
        }

        //destroys coin
        base.OnTriggerEnter2D(c);
    }
}
