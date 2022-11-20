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

    public string gunName;
    public int gunIndex;
    public string gunStat;
    public int gunStatChange;

    public enum StatType {Speed, JumpHeight}
    public StatType playerStat;
    public Char forWhichCharacter;
    public int playerStatChange;
    
    public GameObject newStructure;

    private GameObject player1;
    private GameObject player2;
    public P2Shooting p2Shooting;

    void Start()
    {
        player1 = GameManager.Instance.playerFarmer.gameObject;
        player2 = GameManager.Instance.playerShooter.gameObject;
        p2Shooting = player2.GetComponentInChildren<P2Shooting>();
    }

    public void performAction()
    {
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
        p2Shooting.EquipGun(gunName);
    }

    void upgradeWeapon()
    {
        p2Shooting.allGuns[gunIndex].GetComponent<Gun>().gunInfo.damage += gunStatChange;
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
                    player1.GetComponent<P1Controller>().stats.speed += playerStatChange;
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
                    player2.GetComponent<P2Controller>().stats.speed += playerStatChange;
                    break;
                case StatType.JumpHeight:
                    break;
            }
        }
    }
}
