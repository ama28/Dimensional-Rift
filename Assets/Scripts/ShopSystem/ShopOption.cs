using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Shop Option", menuName = "ScriptableObjects/Shop Option")]
public class ShopOption : ScriptableObject
{
    public enum tier {Bronze, Silver, Gold}

    public bool isForFarmer;
    public tier cardTier;
    public Sprite header;
    public string title;
    public string description;
    public CardAction cardAction;

    public int price;
}
