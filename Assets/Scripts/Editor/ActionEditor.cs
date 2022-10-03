using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardAction))]
public class ActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CardAction cardAction = (CardAction)target;

        cardAction.actionType = (CardAction.ActionType)EditorGUILayout.EnumPopup("ActionType", cardAction.actionType);

        switch (cardAction.actionType)
        {
            case CardAction.ActionType.NewWeapon:
                cardAction.gunName = EditorGUILayout.TextField("Gun Name", cardAction.gunName);
                break;
            case CardAction.ActionType.WeaponUpgrade:
                cardAction.gunIndex = EditorGUILayout.IntField("Gun Index", cardAction.gunIndex);
                cardAction.gunStat = EditorGUILayout.TextField("Gun Stat", cardAction.gunStat);
                cardAction.gunStatChange = EditorGUILayout.IntField("Stat Change Amount", cardAction.gunStatChange);
                break;
            case CardAction.ActionType.NewStructure:
                break;
            case CardAction.ActionType.StatBoost:
                cardAction.isPlayerFarmer = EditorGUILayout.Toggle("Is Farmer Player", cardAction.isPlayerFarmer);
                cardAction.playerStat = EditorGUILayout.TextField("Player Stat", cardAction.playerStat);
                cardAction.playerStatChange = EditorGUILayout.IntField("Stat Change Amount", cardAction.playerStatChange);
                break;
        }
    }
}
