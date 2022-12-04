using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsMenu : MonoBehaviour
{
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI currentHeldAmmo;
    public Image gun0Image;

    public GameObject gun1UI;
    public TextMeshProUGUI gun1Ammo;
    public Image gun1Image;

    public GameObject gun2UI;
    public TextMeshProUGUI gun2Ammo;
    public Image gun2Image;

    private P2Shooting p2Shooting;

    public void Start()
    {
        p2Shooting = FindObjectOfType<P2Shooting>();
    }

    public void Update()
    {
        if (p2Shooting == null)
            return;

        currentAmmo.text = p2Shooting.GetGun(0).GetCurrentAmmo();
        currentAmmo.fontSize = (currentAmmo.text == "\u221E") ? 48 : 36;
        currentHeldAmmo.text = p2Shooting.GetGun(0).GetHeldAmmo();
        currentHeldAmmo.fontSize = (currentAmmo.text == "\u221E") ? 24 : 18;
        gun0Image.sprite = p2Shooting.GetGun(0).gunInfo.uiSprite;

        if (p2Shooting.GetGun(1) == null)
        {
            gun1UI.SetActive(false);
        } else
        {
            gun1UI.SetActive(true);
            gun1Ammo.text = p2Shooting.GetGun(1).GetTotalAmmo();
            gun1Ammo.fontSize = (currentAmmo.text == "\u221E") ? 24 : 18;
            gun1Image.sprite = p2Shooting.GetGun(1).gunInfo.uiSprite;
        }

        if (p2Shooting.GetGun(2) == null)
        {
            gun2UI.SetActive(false);
        }
        else
        {
            gun2UI.SetActive(true);
            gun2Ammo.text = p2Shooting.GetGun(2).GetTotalAmmo();
            gun2Ammo.fontSize = (currentAmmo.text == "\u221E") ? 24 : 18;
            gun2Image.sprite = p2Shooting.GetGun(2).gunInfo.uiSprite;
        }
    }
}
