using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAction : System.Object
{
    public enum ActionType {NewWeapon, WeaponUpgrade, NewStructure, StatBoost}
    public ActionType actionType;
    public int firstIndex;
    public int secondIndex;
    public string label;

    void performAction()
    {
        switch (actionType)
        {
            case ActionType.NewWeapon:
                equipWeapon();
                break;
            case ActionType.WeaponUpgrade:
                break;
            case ActionType.NewStructure:
                break;
            case ActionType.StatBoost:
                break;
        }
    }

    void equipWeapon()
    {

    }

    void upgradeWeapon()
    {

    }

    void buildStructure()
    {

    }

    void boostStat()
    {

    }
}
