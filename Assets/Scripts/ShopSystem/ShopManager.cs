using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField]
    private List<ShopOption> shopOptions;
    [SerializeField]
    private int optionCount = 3;
    [SerializeField]
    private List<Transform> cardSlots = new List<Transform>();
    public Canvas shopUI;
    public GameObject cardPrefab;

    private void Start()
    {
        shopUI = GameObject.FindGameObjectWithTag("ShopUI").GetComponent<Canvas>();
        shopUI.enabled = false;

        //populate shopUI with the cards
        for (int i = 0; i < optionCount; i++)
        {
            GameObject temp = Instantiate(cardPrefab, shopUI.GetComponentInChildren<HorizontalLayoutGroup>().transform);
            cardSlots.Add(temp.transform);
        }
    }

    private void OnEnable()
    {
        GameManager.OnBuildPhaseStart += OpenShop;
    }

    private void OnDisable()
    {
        GameManager.OnBuildPhaseStart -= OpenShop;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        if (GameManager.Instance.Level != 0)
        {
            Time.timeScale = 0f;

            RandomizeShopOptions();

            for (int i = 0; i < cardSlots.Count; i++)
            {
                if (cardSlots[i].GetComponentInChildren<CardAction>() != null)
                {
                    DestroyImmediate(cardSlots[i].GetComponentInChildren<CardAction>().gameObject);
                }

                Instantiate(shopOptions[i].cardAction, cardSlots[i]);

                cardSlots[i].GetComponent<ActionManger>().myAction = cardSlots[i].GetComponentInChildren<CardAction>();

                //frame
                cardSlots[i].GetChild(3).GetComponent<Image>().sprite = shopOptions[i].frame;

                //header
                cardSlots[i].GetChild(2).GetComponent<Image>().sprite = shopOptions[i].header;

                //title
                cardSlots[i].GetChild(4).GetComponent<TextMeshProUGUI>().text = shopOptions[i].title;

                //description
                cardSlots[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = shopOptions[i].description;
            }

            shopUI.enabled = true;
        }
    }
}