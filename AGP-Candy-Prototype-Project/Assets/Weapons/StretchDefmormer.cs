using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class StretchDefmormer : NVRInteractableItem {

    protected override void Start()
    {
        base.Start();

        AllowTwoHanded = true;

        //transform.position = Vector3.zero;

        //Vector3 p1 = new Vector3(1,0,1);
        //Vector3 p2 = new Vector3(-1,0,-1);

        //ScaleToPositions(p1, p2);
    }


    protected override void Update()
    {
        base.Update();

        if(AttachedHands.Count == 2)
        {
            ScaleUpdate();
        }

    }

    private void ScaleUpdate()
    {
        if(AttachedHands.Count == 2)
        {
            ScaleToPositions(AttachedHands[0].CurrentPosition, AttachedHands[1].CurrentPosition);
        }
    }

    
    private void ScaleToPositions(Vector3 position1, Vector3 position2)
    {
        float scale = Vector3.Distance(position1, position2);

        // set scaleAxis to axis between controllers
        Vector3 scaleAxis = position1 - position2;
        transform.forward = scaleAxis;

        transform.localScale = new Vector3(0.3f, 0.3f, scale) ;
    }


}
