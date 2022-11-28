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

        cardAction.forWhichCharacter = (CardAction.Char)EditorGUILayout.EnumPopup("Char Target", cardAction.forWhichCharacter);
        cardAction.cardTier = (CardAction.Tier)EditorGUILayout.EnumPopup("Card Tier", cardAction.cardTier);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Background Sprite");
        cardAction.header = (Sprite)EditorGUILayout.ObjectField(cardAction.header, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();

        cardAction.title = EditorGUILayout.TextField("Title", cardAction.title);
        cardAction.description = EditorGUILayout.TextField("Description", cardAction.description);
        cardAction.price = EditorGUILayout.IntField("Price", cardAction.price);

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
                cardAction.newStructure = (GameObject)EditorGUILayout.ObjectField("New Structure", cardAction.newStructure, typeof(GameObject), true);
                break;
            case CardAction.ActionType.StatBoost:
                cardAction.playerStat = (CardAction.StatType)EditorGUILayout.EnumPopup("Stat Type", cardAction.playerStat);
                cardAction.playerStatChange = EditorGUILayout.IntField("Stat Change Amount", cardAction.playerStatChange);
                break;
        }

        EditorUtility.SetDirty(cardAction);
    }
}
