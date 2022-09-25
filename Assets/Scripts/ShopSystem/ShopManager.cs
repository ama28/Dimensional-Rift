using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    public Canvas shopUI;
    public List<ShopOption> shopOptions = new List<ShopOption>() { };
    public GameObject[] cardSlots;

    private void Start()
    {
        shopUI.enabled = false;
    }

    void RandomizeShopOptions()
    {
        for (int i = shopOptions.Count - 1; i > 0; i--)
        {
            int k = Random.Range(0, shopOptions.Count);
            ShopOption temp = shopOptions[k];
            shopOptions[k] = shopOptions[i];
            shopOptions[i] = temp;
        }
    }

    void OpenShop()
    {
        shopUI.enabled = true;
    }
}
