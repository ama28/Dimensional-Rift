using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinIndicator : MonoBehaviour
{
    public enum coinType {farm, cyber}
    public coinType type;

    // Update is called once per frame
    void Update()
    {
        if (type == coinType.farm)
            gameObject.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.currency.ToString();
        else if (type == coinType.cyber)
            gameObject.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.spaceCurrency.ToString();
    }
}
