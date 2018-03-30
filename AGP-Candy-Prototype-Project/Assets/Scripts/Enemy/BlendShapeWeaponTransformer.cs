using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlendShapeWeaponTransformer : EnemyWeaponTransformer {

    private Weapon weaponScript;
    private WeaponHealth weaponHealthScript;

    [SerializeField]
    private float distForFullTransform = 1.0f;

    [SerializeField]
    private SkinnedMeshRenderer[] blendShapeRenderers;

    protected override void Start()
    {
        Debug.LogWarning("TODO: make this class inherit from a generic Transformer class.");

        weaponScript = GetComponent<Weapon>();
        weaponScript.enabled = false;
        weaponHealthScript = GetComponent<WeaponHealth>();
        weaponHealthScript.enabled = false;
    }

    // Called once when the second hand grabs on to this enemy
    public override void OnSecondHandGrab()
    {
        var s1 = GetComponent<EnemyHealth>();
        if (s1) Destroy(s1);
        var s2 = GetComponent<EnemyAttack>();
        if (s2) Destroy(s2);
        var s3 = GetComponent<EnemyMove>();
        if (s3) Destroy(s3);
        var s4 = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (s4) Destroy(s4);
        
        weaponScript.enabled = true;
        weaponHealthScript.enabled = true;
    }

    // Called in Update every frame while being held by a second hand
    // DON'T LET GO JACK ;(
    public override void OnSecondHandHold(NewtonVR.NVRInteractableItem nvrIItem)
    {
        float distBetweenHands = Vector3.Distance(nvrIItem.AttachedHands[0].transform.position, nvrIItem.AttachedHands[1].transform.position);

        AdjustBlendShapes(distBetweenHands);

        // Stretch Weapon
        ScaleModelToPositions(gameObject, nvrIItem.AttachedHands[0].transform.position, nvrIItem.AttachedHands[1].transform.position);
    }

    // Called once one hand lets go. Aka only 1 hand is left holding on
    public override void OnSecondHandReleased(NewtonVR.NVRHand handStillHolding, NewtonVR.NVRInteractableItem nvrIItem)
    {
        Destroy(this);
    }

    private void AdjustBlendShapes(float distanceBetweenHands)
    {
        float blendScale = Mathf.Min(1, distanceBetweenHands / distForFullTransform) * 100.0f;

        foreach(SkinnedMeshRenderer smr in blendShapeRenderers)
        {
            smr.SetBlendShapeWeight(0, blendScale);
        }
    }

    /// <summary>
    /// Scales the model to the 2 points.
    /// Scales along ALL axiss.
    /// </summary>
    /// <param name="position1">world space</param>
    /// <param name="position2">world space</param>
    private void ScaleModelToPositions(GameObject model, Vector3 position1, Vector3 position2)
    {
        Vector3 middlePosition = (position1 + position2) / 2.0f;
        model.transform.position = middlePosition;

        float scale = Vector3.Distance(position1, position2) / distForFullTransform;

        // Rotate Model to be pointing along the axis
        Vector3 scaleAxis = position1 - position2;
        model.transform.up = scaleAxis;

        // Scale model according to distance between points
        model.transform.localScale = new Vector3(
            scale,
            scale,
            scale);
    }

}
