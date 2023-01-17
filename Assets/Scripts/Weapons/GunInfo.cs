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

    public enum SFXType {
        Gunshot, Sniper, Pistol, Laser
    }

    public FireType fireType = FireType.Normal;
    public SFXType sfxType = SFXType.Gunshot;

    [Tooltip("Max ammo the gun can hold after reloading")]
    public uint clipSize = uint.MaxValue;
    public uint maxAmmo = uint.MaxValue;
    [Tooltip("Amount of time it takes to reload the gun")]
    public float reloadTime = 1f;

    public float fireRate = 0.3f;
    public float range = 10;
    public bool falloff = false;
    public float damage = 1;
    public float bulletSpeed = 10f;

    public float knockback = 10.0f;
    public int pierce = 1;
    public float splashRange = 0.0f;
    [Tooltip("Spread range in degrees")]
    public float accuracy = 0.0f;
    public uint bulletCount = 1;

    public Sprite uiSprite;
}
