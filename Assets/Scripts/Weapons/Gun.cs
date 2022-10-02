using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float timeBetweenFiring;

    private bool canFire_;
    private float timer_;

    void Awake()
    {
       canFire_ = true;
       timer_ = 0;
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
            if (timer_ > timeBetweenFiring)
            {
                canFire_ = true;
                timer_ = 0;
            }
        }
    }
}
