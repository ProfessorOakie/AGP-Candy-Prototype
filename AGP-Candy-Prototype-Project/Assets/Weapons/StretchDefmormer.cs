using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class StretchDefmormer : NVRInteractableItem {

    Renderer[] renderers;

    protected override void Start()
    {
        base.Start();
        AllowTwoHanded = true;
        renderers = GetComponentsInChildren<Renderer>();
    }


    protected override void Update()
    {
        base.Update();

        if(AttachedHands.Count == 2)
        {
            HeldUpdate();
        }

    }

    // Called when held by 2 hands
    private void HeldUpdate()
    {
        if (AttachedHands.Count == 2)
        {
            // Center the object to be exactly in the middle between the hands.
            Vector3 middlePosition = (AttachedHands[0].transform.position + AttachedHands[1].transform.position) / 2.0f;
            transform.position = middlePosition;

            ScaleToPositions(AttachedHands[0].transform.position, AttachedHands[1].transform.position);
        }
    }

    /// <summary>
    /// Stretches the model to the 2 points.
    /// Requires the StretchDeformerShader to be on.
    /// </summary>
    /// <param name="position1">world space</param>
    /// <param name="position2">world space</param>
    private void ScaleToPositions(Vector3 position1, Vector3 position2)
    {
        // Used later to scale based on how far apart your points are
        float scale = Vector3.Distance(position1, position2);

        // Put the positions in model space
        position1 -= transform.position;
        position2 -= transform.position;

        // Normalize
        position1.Normalize();
        position2.Normalize();

        // Inverse rotate the positions by the object's rotation
        // This makes it to the object can have any rotation but will still stretch towards your positions
        position1 = Quaternion.Inverse(Quaternion.Euler(transform.eulerAngles)) * position1;
        position2 = Quaternion.Inverse(Quaternion.Euler(transform.eulerAngles)) * position2;

        // Handle the scaling from the object
        position1.x /= transform.localScale.x;
        position1.y /= transform.localScale.y;
        position1.z /= transform.localScale.z;
        position2.x /= transform.localScale.x;
        position2.y /= transform.localScale.y;
        position2.z /= transform.localScale.z;

        // Scale based on distance between points
        position1 *= scale;
        position2 *= scale;

        // Send the positions to the shader
        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].material.SetVector("p1", position1);
            renderers[i].material.SetVector("p2", position2);
        }
    }

}
