using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopOption
{
    public enum tier {Bronze, Silver, Gold}

    public bool isForFarmer;
    public tier cardTier;
    public Sprite header;
    public string title;
    public string description;
    public GameObject cardAction;

    public int price;
}
