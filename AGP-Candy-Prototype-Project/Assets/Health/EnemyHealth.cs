﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    Renderer[] renderers;
    List<Color> oldColors = new List<Color>();

    protected override void Start()
    {
        base.Start();
        renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; ++i)
        {
            oldColors.Add(renderers[i].material.color);
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        //StartCoroutine(FlashRed(1.0f));

        foreach(var rend in renderers)
        {
            rend.material.SetFloat("_FillPercent", 1.0f - (mCurrentHealth / mMaxHealth));
        }
    }


    private IEnumerator FlashRed(float duration)
    {
        for (int i = 0; i < renderers.Length; ++i)
            renderers[i].material.color = Color.red;

        float oldHealth = mCurrentHealth;
        yield return new WaitForSeconds(duration);

        if (oldHealth == mCurrentHealth)
            for (int i = 0; i < renderers.Length; ++i)
                renderers[i].material.color = oldColors[i];

    }

}
