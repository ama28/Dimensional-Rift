using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBuilding : Building, Interactable
{
    bool isHeld;
    protected int mFrames = 0;

    public virtual void OnInteract(Player player) {
        isHeld = true;

        transform.SetParent(player.transform);
    }

    public virtual void OnRelease(Player player) {
        isHeld = false;
        UpdateGraph();

        transform.SetParent(GameManager.Instance.BuildingManager.transform);
    }

    void Update() {
        if (isHeld)
        {
            mFrames++;

            if (mFrames >= 30)
            {
                UpdateGraph();
                mFrames = 0;
            }
        }
    }

    protected override void UpdateGraph()
    {
        base.UpdateGraph();
        mFrames = 0;
    }
}
