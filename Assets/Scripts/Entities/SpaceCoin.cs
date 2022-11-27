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
            GameManager.Instance.spaceCurrency++;
            AudioManager.i.Coin(2);
            base.OnTriggerEnter2D(c); //destroys coin
        }
        
    }
}
