using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardAction : MonoBehaviour
{
    public enum ActionType {NewWeapon, WeaponUpgrade, NewStructure, StatBoost}
    public ActionType actionType;

    public string gunName;
    public int gunIndex;
    public string gunStat;
    public int gunStatChange;

    public bool isPlayerFarmer;
    public string playerStat;
    public int playerStatChange;

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

    }

    void boostStat()
    {
        if (isPlayerFarmer)
        {
            switch (playerStat)
            {
                case "speed":
                    break;
                case "jumpHeight":
                    break;
            }
        }
        else
        {
            switch (playerStat)
            {
                case "speed":
                    player2.GetComponent<P2Controller>().stats.speed += playerStatChange;
                    break;
                case "jumpHeight":
                    break;
            }
        }
    }
}
