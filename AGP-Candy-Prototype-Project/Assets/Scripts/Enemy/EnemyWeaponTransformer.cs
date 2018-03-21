using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this on the Enemy Prefab
public class EnemyWeaponTransformer : MonoBehaviour {

    Renderer[] renderers;

    [SerializeField]
    private GameObject weaponPrefab;

    private GameObject spawnedWeapon;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Called once when the second hand grabs on to this enemy
    public void OnSecondHandGrab()
    {
        SpawnWeapon();
    }

    // Called in Update every frame while being held by a second hand
    // DON'T LET GO JACK ;(
    public void OnSecondHandHold(NewtonVR.NVRInteractableItem nvrIItem)
    {
        // Stretch Enemy
        StretchModelToPositions(nvrIItem.AttachedHands[0].transform.position, nvrIItem.AttachedHands[1].transform.position);

        // Stretch Weapon
        if(spawnedWeapon)
            ScaleModelToPositions(spawnedWeapon, nvrIItem.AttachedHands[0].transform.position, nvrIItem.AttachedHands[1].transform.position);
    }

    // Called once one hand lets go. Aka only 1 hand is left holding on
    public void OnSecondHandReleased(NewtonVR.NVRHand handStillHolding, NewtonVR.NVRInteractableItem nvrIItem)
    {
        // End interaction with Enemy
        handStillHolding.EndInteraction(nvrIItem);

        // Start Interaction with Weapon
        handStillHolding.BeginInteraction(spawnedWeapon.GetComponent<NewtonVR.NVRInteractable>());

        // Destroy Enemy
        Destroy(gameObject);

    }

    private void SpawnWeapon()
    {
        // Instantiate
        spawnedWeapon = Instantiate(weaponPrefab);

        // TODO effects
    }

    /// <summary>
    /// Stretches the model to the 2 points.
    /// Requires the StretchDeformerShader to be on.
    /// </summary>
    /// <param name="position1">world space</param>
    /// <param name="position2">world space</param>
    private void StretchModelToPositions(Vector3 position1, Vector3 position2)
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


    /// <summary>
    /// Scales the model to the 2 points.
    /// </summary>
    /// <param name="position1">world space</param>
    /// <param name="position2">world space</param>
    private void ScaleModelToPositions(GameObject model, Vector3 position1, Vector3 position2)
    {
        float scale = Vector3.Distance(position1, position2);

        // Rotate Model to be pointing along the axis
        Vector3 scaleAxis = position1 - position2;
        model.transform.forward = scaleAxis;

        // Scale model according to distance between points
        model.transform.localScale = new Vector3(
            model.transform.localScale.x,
            model.transform.localScale.y, 
            scale);
    }

}
