using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class Enemy_InteractableItem : NVRInteractableItem
{
    private EnemyWeaponTransformer mTransformer;

    public override void BeginInteraction(NVRHand hand)
    {
        base.BeginInteraction(hand);

        if( AttachedHands.Count == 2)
        {
            if(mTransformer != null)
                mTransformer.OnSecondHandGrab();
        }

    }

    public override void InteractingUpdate(NVRHand hand)
    {
        base.InteractingUpdate(hand);

        if (AttachedHands.Count == 2)
        {
            if (mTransformer != null)
                mTransformer.OnSecondHandHold(this);
        }
    }

    public override void EndInteraction(NVRHand hand)
    {
        base.EndInteraction(hand);

        if (AttachedHands.Count == 1)
        {
            if (mTransformer != null)
                mTransformer.OnSecondHandReleased(AttachedHand);
        }

    }

}
