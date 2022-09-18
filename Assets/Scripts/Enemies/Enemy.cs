using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //basic enemy initialization

        
    }

    protected abstract void Attack();
    protected virtual void Move() {
        //pathfinding shiz, possibly do setup in here and then specific behaviors in derived class

    }

    protected virtual void Die() {
        Destroy(this);
    }

}
