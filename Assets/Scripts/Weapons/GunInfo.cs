using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunInfo
{
    public enum FireType {
        Normal = 0,
        Continuous = 1
    }

    public FireType fireType = FireType.Normal;

    public float fireRate = 0.3f;
    public float range = 10;
    public bool falloff = false;
    public float damage = 1;
    public float bulletSpeed = 10f;

    public float knockback = 10.0f;
    public int pierce = 1;
    public float splashRange = 0.0f;
    [Tooltip("Spread range in degrees (TODO: maybe change later)")]
    public float accuracy = 0.0f;
    public int bulletCount = 1;
}
