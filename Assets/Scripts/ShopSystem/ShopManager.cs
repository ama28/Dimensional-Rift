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

    public Sprite[] farmerFrames;
    public Sprite[] shooterFrames;

    public GameObject waveCompletedText;

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
        GameManager.OnBuildPhaseStart += waveEnd;
    }

    private void OnDisable()
    {
        GameManager.OnBuildPhaseStart -= waveEnd;
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

    public void waveEnd()
    {
        if (GameManager.Instance.Level > 1)
        {
            StartCoroutine(showWaveComplete());
        }
    }

    IEnumerator showWaveComplete()
    {
        waveCompletedText.SetActive(true);
        yield return new WaitForSeconds(3f);
        waveCompletedText.SetActive(false);
        OpenShop();
    }

    public void skipShop()
    {
        shopUI.enabled = false;
        Time.timeScale = 1f;
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
                cardSlots[i].GetComponent<ActionManger>().cost = shopOptions[i].price;

                //frame
                Image farmerFrame = cardSlots[i].GetChild(2).GetChild(0).GetComponent<Image>();
                Image shooterFrame = cardSlots[i].GetChild(2).GetChild(1).GetComponent<Image>();

                farmerFrame.enabled = shopOptions[i].isForFarmer;
                shooterFrame.enabled = !shopOptions[i].isForFarmer;

                switch (shopOptions[i].cardTier)
                {
                    case ShopOption.tier.Bronze:
                        if (shopOptions[i].isForFarmer)
                            farmerFrame.sprite = farmerFrames[0];
                        else
                            shooterFrame.sprite = shooterFrames[0];
                        break;
                    case ShopOption.tier.Silver:
                        if (shopOptions[i].isForFarmer)
                            farmerFrame.sprite = farmerFrames[1];
                        else
                            shooterFrame.sprite = shooterFrames[1];
                        break;
                    case ShopOption.tier.Gold:
                        if (shopOptions[i].isForFarmer)
                            farmerFrame.sprite = farmerFrames[2];
                        else
                            shooterFrame.sprite = shooterFrames[2];
                        break;
                }

                //header
                cardSlots[i].GetChild(1).GetComponent<Image>().sprite = shopOptions[i].header;

                //title
                cardSlots[i].GetChild(3).GetComponent<TextMeshProUGUI>().text = shopOptions[i].title;

                //description
                cardSlots[i].GetChild(4).GetComponent<TextMeshProUGUI>().text = shopOptions[i].description;

                //price
                cardSlots[i].GetChild(6).GetComponent<TextMeshProUGUI>().text = shopOptions[i].price.ToString();
            }

            shopUI.enabled = true;
        }
    }
}