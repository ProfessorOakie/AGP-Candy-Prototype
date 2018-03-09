using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClubWeapon : Weapon {

    //this is the damage scale of the "Sweet Spot"
    [SerializeField]
    private float mClubDamageScale;
    private SphereCollider clubCollider;
    // Use this for initialization
    protected override void Start()
    {
        clubCollider = GetComponent<SphereCollider>();
        base.Start();
    }

    protected override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") &&
            (collision.contacts[0].thisCollider.Equals(clubCollider)))
        {
            var enemy = collision.gameObject;
            enemy.GetComponent<EnemyHealth>().TakeDamage((collision.relativeVelocity.magnitude) * mClubDamageScale);
            GetComponent<WeaponHealth>().TakeDamage(1);
            if(interactableItem.AttachedHand != null)
            {
                rumbling = true;
                interactableItem.AttachedHand.TriggerHapticPulse(1000, NewtonVR.NVRButtons.Touchpad);
            }

           
        }
        else
        {
            base.OnCollisionEnter(collision);
        }

    }

 


}
