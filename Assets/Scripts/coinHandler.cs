using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class coinHandler : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D c){
        // If player collects coin
        if(c.gameObject.tag == "Player"){
            GameObject.Find("CoinSpawner").GetComponent<CoinSpawnHandler>().SpawnCoin(); // Spawn new coin
            GameObject.Find("CoinSpawner").GetComponent<CoinSpawnHandler>().DecrementCoinCount(); // Decrement coin
            Destroy(this.gameObject); // Destroy this coin
        }
    }
}
