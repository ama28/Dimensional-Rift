using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBuilding : Building, Interactable
{
    bool isHeld;

    public virtual void OnInteract(Player player) {
        isHeld = true;

        transform.SetParent(player.transform);
    }

    public virtual void OnRelease(Player player) {
        isHeld = false;

        transform.SetParent(GameManager.Instance.BuildingManager.transform);
    }

    void Update() {
        if(isHeld) {
            //regen map
        }
    }
}
