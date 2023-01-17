using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpaceCoin : Collectible
{

    void Start() {
        
    }
    public override void OnTriggerEnter2D(Collider2D c){
        // If player collects coin
        if(c.gameObject.tag == "PlayerFarmer"){
            AudioManager.Instance.Coin(1);
            GameManager.Instance.spaceCurrency++;
            // AudioManager.Instance.Coin(2);
            base.OnTriggerEnter2D(c); //destroys coin
        }
        
    }
}
