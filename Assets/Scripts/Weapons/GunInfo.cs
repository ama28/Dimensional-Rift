using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunInfo 
{
    public float fireRate = 0.3f;
    public float range = 10; //TODO: implement
    public float damage = 1;
    public float bulletSpeed = 10f;

    public float knockback = 10.0f;
    public int pierce = 1;
    public float splashRange = 0.0f; //TODO: implement
    public float accuracy = 0.0f; //TODO: implement
}
