﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField]
    public GunInfo gunInfo = new GunInfo();
    
    public GameObject bullet;
    public Being owner;

    protected uint currentAmmo;
    protected uint heldAmmo;

    private bool canFire_;
    private float timer_;

    [HideInInspector]
    public bool gunActive;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
       canFire_ = true;
       timer_ = 0;
    }

    protected virtual void Start()
    {
        bullet.GetComponent<Bullet>().SetUpBullet(owner, gunInfo);
        currentAmmo = gunInfo.clipSize;
        heldAmmo = gunInfo.maxAmmo;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = gunActive;
    }

    public bool CanFire()
    {
        return canFire_;
    }

    public float GetTimer()
    {
        return timer_;
    }

    public string GetCurrentAmmo()
    {
        return (currentAmmo > 999) ? "\u221E" : currentAmmo.ToString();
    }

    public string GetHeldAmmo()
    {
        return (heldAmmo > 999) ? "\u221E" : heldAmmo.ToString();
    }

    public string GetTotalAmmo()
    {
        uint totalAmmo = (uint)(Mathf.Min(currentAmmo, int.MaxValue / 2)) + (uint)(
            Mathf.Min(heldAmmo, int.MaxValue / 2));
        return (totalAmmo > 999*2) ? "\u221E" : totalAmmo.ToString();
    }

    public void Fire()
    {
        if (CanFire())
        {
            canFire_ = false;

            Vector3 position = transform.position;
            for (int i = 0; i < Mathf.Max(1, gunInfo.bulletCount); i++)
            {
                GameObject newBullet = Instantiate(bullet, position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().SetUpBullet(owner, gunInfo);
                AimBullet(newBullet);
            }

            if (currentAmmo < int.MaxValue)
                currentAmmo--;
        }
    }

    private void AimBullet(GameObject bullet)
    {
        bullet.transform.rotation = transform.rotation;
        float angle = (transform.rotation.eulerAngles.z
            + Random.Range(-gunInfo.accuracy, gunInfo.accuracy)) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bullet.GetComponent<Bullet>().speed;
    }

    public void UpdateTimer()
    {
        if (!canFire_)
        {
            timer_ += Time.deltaTime;
            if (timer_ > gunInfo.fireRate && currentAmmo > 0)
            {
                canFire_ = true;
                timer_ = 0;
            }
        }
    }
}
