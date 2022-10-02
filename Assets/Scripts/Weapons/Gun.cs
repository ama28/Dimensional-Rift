using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField]
    public GunInfo gunInfo = new GunInfo();
    
    public GameObject bullet;
    public Being owner;

    private bool canFire_;
    private float timer_;

    void Awake()
    {
       canFire_ = true;
       timer_ = 0;
    }

    protected virtual void Start()
    {
        bullet.GetComponent<Bullet>().SetUpBullet(owner, gunInfo);
    }

    public bool CanFire()
    {
        return canFire_;
    }

    public float GetTimer()
    {
        return timer_;
    }

    public GameObject Fire()
    {
        canFire_ = false;
        return bullet;
    }

    public void UpdateTimer()
    {
        if (!canFire_)
        {
            timer_ += Time.deltaTime;
            if (timer_ > gunInfo.fireRate)
            {
                canFire_ = true;
                timer_ = 0;
            }
        }
    }
}
