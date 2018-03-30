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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;
            enemy.GetComponent<EnemyHealth>().TakeDamage((collision.relativeVelocity.magnitude) * mDamageScale);
            Debug.LogWarning("Hardcoded Weapon Damage");
            GetComponent<WeaponHealth>().TakeDamage(1);

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
