using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health {

    [SerializeField]
    private float WeaponDamagePerTime = 1;
    [SerializeField]
    private float TimeBetweenDamages = 0.5f;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("DamageWeapon", 0.1f, TimeBetweenDamages);
    }

    private void DamageWeapon()
    {
        TakeDamage(WeaponDamagePerTime);
    }

}
