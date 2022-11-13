using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBuilding : Building, Interactable
{
    bool isHeld;
    protected int mFrames = 0;
    protected Vector3 mPrevPos;

    protected override void Start()
    {
        base.Start();
        mPrevPos = transform.position;
    }

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

            if (mFrames >= 40
                || Vector3.Magnitude(transform.position - mPrevPos) > 5f)
            {
                UpdateGraph();
                mFrames = 0;
            }
        }
    }

    protected override void UpdateGraph()
    {
        base.UpdateGraphThrough(mPrevPos);
        mFrames = 0;
        mPrevPos = transform.position;
    }
}
