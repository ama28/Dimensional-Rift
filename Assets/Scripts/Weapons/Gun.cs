using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GunSFXType {
    Laser, Sniper
}

public class Gun : MonoBehaviour
{
    [SerializeField]
    public GunInfo gunInfo = new GunInfo();
    public GameObject bullet;
    public Being owner;
    public GunSFXType sfxType;

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

    public void Fire()
    {
        if (CanFire())
        {
            canFire_ = false;

            AudioManager.Instance.FireGun(sfxType);

            Vector3 position = transform.position;
            for (int i = 0; i < Mathf.Max(1, gunInfo.bulletCount); i++)
            {
                GameObject newBullet = Instantiate(bullet, position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().SetUpBullet(owner, gunInfo);
                AimBullet(newBullet);
            }
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
            if (timer_ > gunInfo.fireRate)
            {
                canFire_ = true;
                timer_ = 0;
            }
        }
    }
}
