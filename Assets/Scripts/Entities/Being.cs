using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    //general class, anything that has health and a transform, so players and enemies
    //this is here so that we can make a generalized hit class
    //and we can use its transform

    //players, enemies, buildings
    public float health;
    protected bool invulnerable = false;

    public virtual void TakeDamage(HitInfo hit) {

        Debug.Log("in takedam low");
        if(!invulnerable) {
            health -= hit.damage;
            Debug.Log(hit.damage);
        }
    }

}
