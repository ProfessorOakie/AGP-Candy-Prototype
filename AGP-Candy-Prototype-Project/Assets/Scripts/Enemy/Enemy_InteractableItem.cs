using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class Enemy_InteractableItem : NVRInteractableItem
{
    private EnemyWeaponTransformer mTransformer;
    private EnemyMove mMover;

    protected override void Start()
    {
        base.Start();
        mTransformer = GetComponent<EnemyWeaponTransformer>();
        mMover = GetComponent<EnemyMove>();
        AllowTwoHanded = true;
    }

    public override void BeginInteraction(NVRHand hand)
    {
        base.BeginInteraction(hand);
        //Debug.Log("BeginInteraction: " + hand + ", numhands: " + AttachedHands.Count);

        mMover.PickedUp();

        if( AttachedHands.Count == 2)
        {
            if(mTransformer != null)
                mTransformer.OnSecondHandGrab();
        }

    }

    public override void InteractingUpdate(NVRHand hand)
    {
        base.InteractingUpdate(hand);
        //Debug.Log("Interacting: numhands: " + AttachedHands.Count);

        if (AttachedHands.Count == 2)
        {
            if (mTransformer != null)
                mTransformer.OnSecondHandHold(this);
        }
    }

    public override void EndInteraction(NVRHand hand)
    {
        base.EndInteraction(hand);
        //Debug.Log("EndInteraction: " + hand + " numhands: " + AttachedHands.Count);

        mMover.Dropped();

        if (AttachedHands.Count == 1)
        {
            if (mTransformer != null)
                mTransformer.OnSecondHandReleased(AttachedHand, this);
        }

    }

}
