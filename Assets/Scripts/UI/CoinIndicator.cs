using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinIndicator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.currency.ToString();
    }
}
