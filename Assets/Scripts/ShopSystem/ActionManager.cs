using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [HideInInspector] public CardAction myAction;
    [HideInInspector] public int cost;

    public void Activate()
    {
        switch (myAction.forWhichCharacter){
            case CardAction.Char.farmer: 
                if (GameManager.Instance.currency >= cost)
                {
                    myAction.performAction();
                    gameObject.GetComponentInParent<Canvas>().enabled = false;
                    GameManager.Instance.currency = Mathf.Clamp(GameManager.Instance.currency - cost, 0, 999);
                    Time.timeScale = 1f;
                } else Debug.Log("can't afford");
                break;
            case CardAction.Char.shooter: 
                if (GameManager.Instance.spaceCurrency >= cost)
                {
                    myAction.performAction();
                    gameObject.GetComponentInParent<Canvas>().enabled = false;
                    GameManager.Instance.currency = Mathf.Clamp(GameManager.Instance.spaceCurrency - cost, 0, 999);
                    Time.timeScale = 1f;
                } else Debug.Log("can't afford");
                break;
        };
    }
}
