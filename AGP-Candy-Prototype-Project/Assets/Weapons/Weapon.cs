using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField]
    protected float mDamageScale;
    protected NewtonVR.NVRInteractableItem interactableItem;
    protected bool rumbling;

    protected virtual void Start()
    {
        interactableItem = GetComponent<NewtonVR.NVRInteractableItem>();
    }

    //protected virtual void Update()
    //{
    //    if (rumbling && interactableItem.AttachedHand != null)
    //    {
    //        interactableItem.AttachedHand.TriggerHapticPulse(500);
    //    }
    //}
    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!isActiveAndEnabled) return;

        var enemy = collision.gameObject;
        var enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth)
        {
            float damage = (collision.relativeVelocity.magnitude) * mDamageScale;
            enemyHealth.TakeDamage(damage);
            GetComponent<WeaponHealth>().TakeDamage(damage);

            if (interactableItem == null)
                interactableItem = GetComponent<NewtonVR.NVRInteractableItem>();
            if (interactableItem != null)
            {
                if (interactableItem.AttachedHand != null)
                {
                    rumbling = true;

                    interactableItem.AttachedHand.TriggerHapticPulse(1000, NewtonVR.NVRButtons.Touchpad);

                }
            }
        }
    }
}
