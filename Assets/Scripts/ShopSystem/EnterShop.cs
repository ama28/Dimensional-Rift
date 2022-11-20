using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    [SerializeField]
    private ShopManager.ShopType shopType;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (shopType == ShopManager.ShopType.farmer && col.tag == "PlayerFarmer")
        {
            GameManager.Instance.shopManager.OpenShop(shopType);
        }
        else if (shopType == ShopManager.ShopType.shooter && col.tag == "PlayerShooter")
        {
            GameManager.Instance.shopManager.OpenShop(shopType);
        }
    }
}
