using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerFarmer")
            Destroy(gameObject);
    }
}
