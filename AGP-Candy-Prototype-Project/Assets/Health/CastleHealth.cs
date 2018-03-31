using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleHealth : Health {

    Renderer[] renderers;
    List<Color> oldColors = new List<Color>();

    [SerializeField]
    private Transform AttackPoint;

    protected override void Start()
    {
        base.Start();
        renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; ++i)
        {
            oldColors.Add(renderers[i].material.color);
        }
    }

    public Transform GetAttackPoint()
    {
        return AttackPoint;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        StartCoroutine(FlashBlue(1.0f));
    }

	public override void Die ()
	{
		SceneManager.LoadScene("Death");
	}


    private IEnumerator FlashBlue(float duration)
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
