using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField]
    private List<CardAction> allOptions;
    [HideInInspector]
    public List<CardAction> farmerShopOptions;
    [HideInInspector]
    public List<CardAction> shooterShopOptions;
    [SerializeField]
    private int optionCount = 3;
    [SerializeField]
    private List<Transform> cardTransforms = new List<Transform>();
    public Canvas shopUI;
    public GameObject cardPrefab;

    public Sprite[] farmerFrames;
    public Sprite[] shooterFrames;

    public TMP_FontAsset farmFont;
    public TMP_FontAsset farmFontBold;
    public TMP_FontAsset cyberFont;
    public TMP_FontAsset cyberFontBold;

    public GameObject waveCompletedText;

    public Sprite farmerCoinIcon;
    public Sprite shooterCoinIcon;

    public enum ShopType {farmer, shooter};

    private void Start()
    {
        shopUI = GameObject.FindGameObjectWithTag("ShopUI").GetComponent<Canvas>();
        shopUI.enabled = false;

        //populate shopUI with the cards
        for (int i = 0; i < optionCount; i++)
        {
            GameObject temp = Instantiate(cardPrefab, shopUI.GetComponentInChildren<HorizontalLayoutGroup>().transform);
            cardTransforms.Add(temp.transform);
        }

        foreach (CardAction option in allOptions){
            if (option.forWhichCharacter == CardAction.Char.farmer) 
                farmerShopOptions.Add(option);
            else shooterShopOptions.Add(option);
        }
    }

    void OnEnable()
    {
        GameManager.OnBuildPhaseStart += waveEnd;
    }

    void OnDisable()
    {
        GameManager.OnBuildPhaseStart -= waveEnd;
    }

    void RandomizeShopOptions(ShopType shopType)
    {
        List<CardAction> shopOptions;

        if (shopType == ShopType.farmer) shopOptions = farmerShopOptions;
        else shopOptions = shooterShopOptions;
        
        for (int i = shopOptions.Count - 1; i > 0; i--)
        {
            int k = Random.Range(0, shopOptions.Count);
            CardAction temp = shopOptions[k];
            shopOptions[k] = shopOptions[i];
            shopOptions[i] = temp;
        }

        if (shopType == ShopType.farmer) farmerShopOptions = shopOptions;
        else shooterShopOptions = shopOptions;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OpenShop(ShopType.farmer);
            GameManager.Instance.currency += 50;
        } else if (Input.GetKeyDown(KeyCode.K))
        {
            OpenShop(ShopType.shooter);
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
        //OpenShop();
    }

    public void skipShop()
    {
        shopUI.enabled = false;
        Time.timeScale = 1f;
    }

    public void OpenShop(ShopType shopType)
    {
        // don't open on first level 
        // if (GameManager.Instance.Level != 0)
        {
            // pause gameplay
            Time.timeScale = 0f;

            // randomize our shop options so we can show the first 3
            RandomizeShopOptions(shopType);

            List<CardAction> shopOptions;
            bool isFarmerShop;

            if (shopType == ShopType.farmer)
            {
                shopOptions = farmerShopOptions;
                isFarmerShop = true;
            }
            else // shopType == ShopType.shooter
            {
                shopOptions = shooterShopOptions;
                isFarmerShop = false;
            }

            for (int i = 0; i < cardTransforms.Count; i++)
            {
                // if (cardTransforms[i].GetComponentInChildren<CardAction>() != null)
                // {
                //     DestroyImmediate(cardTransforms[i].GetComponentInChildren<CardAction>());
                // }

                // if (isFarmerShop) Instantiate(farmerShopOptions[i], cardTransforms[i]);
                // else Instantiate(shooterShopOptions[i], cardTransforms[i]);

                cardTransforms[i].GetComponent<ActionManager>().myAction = shopOptions[i];
                cardTransforms[i].GetComponent<ActionManager>().cost = shopOptions[i].price;

                //frame
                Image farmerFrame = cardTransforms[i].GetChild(2).GetChild(0).GetComponent<Image>();
                Image shooterFrame = cardTransforms[i].GetChild(2).GetChild(1).GetComponent<Image>();

                switch (shopOptions[i].cardTier)
                {
                    case CardAction.Tier.Bronze:
                        if (isFarmerShop)
                            farmerFrame.sprite = farmerFrames[0];
                        else
                            shooterFrame.sprite = shooterFrames[0];
                        break;
                    case CardAction.Tier.Silver:
                        if (isFarmerShop)
                            farmerFrame.sprite = farmerFrames[1];
                        else
                            shooterFrame.sprite = shooterFrames[1];
                        break;
                    case CardAction.Tier.Gold:
                        if (isFarmerShop)
                            farmerFrame.sprite = farmerFrames[2];
                        else
                            shooterFrame.sprite = shooterFrames[2];
                        break;
                }

                if (isFarmerShop){
                    shooterFrame.enabled = false;
                    farmerFrame.enabled = true;
                } else {
                    shooterFrame.enabled = true;
                    farmerFrame.enabled = false;
                }

                //header
                cardTransforms[i].GetChild(1).GetComponent<Image>().sprite = shopOptions[i].header;

                //title
                cardTransforms[i].GetChild(3).GetComponent<TextMeshProUGUI>().text = shopOptions[i].title;
                cardTransforms[i].GetChild(3).GetComponent<TextMeshProUGUI>().font = isFarmerShop ? farmFontBold : cyberFontBold;
                cardTransforms[i].GetChild(3).GetComponent<TextMeshProUGUI>().fontSize = isFarmerShop ? 22 : 15;

                //description
                cardTransforms[i].GetChild(4).GetComponent<TextMeshProUGUI>().text = shopOptions[i].description;
                cardTransforms[i].GetChild(4).GetComponent<TextMeshProUGUI>().font = isFarmerShop ? farmFont : cyberFont;

                //price
                cardTransforms[i].GetChild(6).GetComponent<TextMeshProUGUI>().text = "$" + shopOptions[i].price.ToString();
            }

            //change card and button font
            shopUI.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().font = isFarmerShop ? farmFont : cyberFont;
            shopUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = isFarmerShop ? farmerCoinIcon : shooterCoinIcon;
            shopUI.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().font = isFarmerShop ? farmFont : cyberFont;
            shopUI.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = isFarmerShop ? 30 : 20;
            shopUI.transform.GetChild(2).GetChild(2).GetComponent<CoinIndicator>().type = isFarmerShop ? CoinIndicator.coinType.farm : CoinIndicator.coinType.cyber;

            shopUI.enabled = true;
        }
    }
}