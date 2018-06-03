using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health {
    Renderer[] renderers;
    [SerializeField]
    private float WeaponDamagePerTime = 1;
    [SerializeField]
    private float TimeBetweenDamages = 0.5f;

    protected override void Start()
    {
        base.Start();
        renderers = GetComponentsInChildren<Renderer>();
        for(int i = 0; i<renderers.Length; i++)
        {
            foreach(var mat in renderers[i].materials)
            {
                mat.SetInt("_FillAxis", 4);
            }
        }
        InvokeRepeating("DamageWeapon", 0.1f, TimeBetweenDamages);
    }

    private void DamageWeapon()
    {
        TakeDamage(WeaponDamagePerTime);
        foreach (var rend in renderers)
        {
            foreach(var mat in rend.materials)
            {
                mat.SetFloat("_FillPercent", 1.0f - (mCurrentHealth / mMaxHealth));
            }
        }
    }

}
