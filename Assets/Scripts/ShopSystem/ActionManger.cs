using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManger : MonoBehaviour
{
    public CardAction myAction;
    public int cost;

    public void Activate()
    {
        if (GameManager.Instance.currency >= cost)
        {
            myAction.performAction();
            gameObject.GetComponentInParent<Canvas>().enabled = false;
            GameManager.Instance.currency = Mathf.Clamp(GameManager.Instance.currency - cost, 0, 100);
            Time.timeScale = 1f;
        }
        Debug.Log("can't afford");
    }
}
