using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopOption : System.Object
{
    public Sprite frame;
    public Sprite header;
    public string title;
    public string description;

    //add stat changes / new items / effects

    private void Start()
    {
        //Image[] images = GetComponentsInChildren<Image>();

        //if (images.Length < 2)
        //    Debug.Log("frame or header missing from shop option");
        //else
        //{
        //    frame = images[0].sprite;
        //    header = images[1].sprite;
        //}
    }
}
