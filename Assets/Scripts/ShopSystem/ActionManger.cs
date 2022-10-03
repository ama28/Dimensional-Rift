using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManger : MonoBehaviour
{
    public CardAction myAction;

    public void Activate()
    {
        myAction.performAction();
    }
}
