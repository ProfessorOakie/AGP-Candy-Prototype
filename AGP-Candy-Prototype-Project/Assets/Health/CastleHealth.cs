using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health {

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
        StartCoroutine(FlashBlue(1.0f));
    }


    private IEnumerator FlashBlue(float duration)
    {
        for (int i = 0; i < renderers.Length; ++i)
            renderers[i].material.color = Color.blue;

        float oldHealth = mCurrentHealth;
        yield return new WaitForSeconds(duration);

        if (oldHealth == mCurrentHealth)
            for (int i = 0; i < renderers.Length; ++i)
                renderers[i].material.color = oldColors[i];

    }

}
