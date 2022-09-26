using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Canvas shopUI;
    public List<ShopOption> shopOptions = new List<ShopOption>() { };
    public List<Transform> cardSlots;

    private void Start()
    {
        shopUI.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            OpenShop();
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
        RandomizeShopOptions();

        foreach (Transform child in shopUI.transform)
        {
            cardSlots.Add(child);
        }

        for (int i = 0; i < cardSlots.Count; i++)
        {
            //cardSlots[i].GetChild(0).GetComponent<Image>().sprite = shopOptions[i].frame;
            //cardSlots[i].GetChild(1).GetComponent<Image>().sprite = shopOptions[i].header;
            cardSlots[i].GetChild(2).GetComponent<TextMeshProUGUI>().text = shopOptions[i].description;
        }

        shopUI.enabled = true;
    }
}
