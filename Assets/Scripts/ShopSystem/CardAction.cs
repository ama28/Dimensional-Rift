using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Action", menuName = "ScriptableObjects/Card Action")]
public class CardAction : ScriptableObject
{
    // shop appearance
    public enum Tier {Bronze, Silver, Gold}

    public Tier cardTier;
    public Sprite header;
    public string title;
    public string description;
    public int price;

    // card action functionality
    public enum ActionType {NewWeapon, WeaponUpgrade, NewStructure, StatBoost}
    public ActionType actionType;
    public enum Char {farmer, shooter}
    public Char forWhichCharacter;

    public string gunName;
    public int gunIndex;
    public string gunStat;
    public int gunStatChange;

    public enum StatType {Speed, JumpHeight}
    public StatType playerStat;
    public int playerStatChange;
    
    public GameObject newStructure;

    public void performAction()
    {
        AudioManager.Instance.ShopClick();
        switch (actionType)
        {
            case ActionType.NewWeapon:
                equipWeapon();
                break;
            case ActionType.WeaponUpgrade:
                upgradeWeapon();
                break;
            case ActionType.NewStructure:
                buildStructure();
                break;
            case ActionType.StatBoost:
                boostStat();
                break;
        }
        Debug.Log(actionType);
    }

    void equipWeapon()
    {
        GameManager.Instance.playerShooter.gameObject.GetComponentInChildren<P2Shooting>().EquipGun(gunName);
    }

    void upgradeWeapon()
    {
        GameManager.Instance.playerShooter.gameObject.GetComponentInChildren<P2Shooting>().allGuns[gunIndex].GetComponent<Gun>().gunInfo.damage += gunStatChange;
    }

    void buildStructure()
    {
        GameManager.Instance.BuildingManager.AddBuildingToInventory(newStructure);
    }

    void boostStat()
    {
        if (forWhichCharacter == Char.farmer)
        {
            switch (playerStat)
            {
                case StatType.Speed:
                    GameManager.Instance.playerFarmer.gameObject.GetComponent<P1Controller>().stats.speed += playerStatChange;
                    break;
                case StatType.JumpHeight:
                    break;
            }
        }
        else // forWhichCharacter == Char.shooter
        {
            switch (playerStat)
            {
                case StatType.Speed:
                    GameManager.Instance.playerShooter.gameObject.GetComponent<P2Controller>().stats.speed += playerStatChange;
                    break;
                case StatType.JumpHeight:
                    GameManager.Instance.playerShooter.gameObject.GetComponent<P2Controller>().stats.jumpForce += playerStatChange;
                    break;
            }
        }
    }
}
