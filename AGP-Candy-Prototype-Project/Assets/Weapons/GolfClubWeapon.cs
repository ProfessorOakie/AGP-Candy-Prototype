using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClubWeapon : Weapon
{

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
        var enemy = collision.gameObject;
        var enemyHealth = enemy.GetComponent<EnemyHealth>();

        Debug.LogWarning("TODO: cleanup this polymorphism");

        if (enemyHealth && (collision.contacts[0].thisCollider.Equals(clubCollider)))
        {
            float damage = (collision.relativeVelocity.magnitude) * mClubDamageScale;
            enemyHealth.TakeDamage(damage);
            GetComponent<WeaponHealth>().TakeDamage(damage);

            if (interactableItem.AttachedHand != null)
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
