using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleHealth : Health {

    Renderer[] renderers;
    List<Color> oldColors = new List<Color>();

    [SerializeField]
    private Transform AttackPoint;

    private AudioSource mSource;
    [SerializeField]
    private AudioClip mClipTakeDamage;
    [SerializeField]
    private AudioClip mClipDie;

    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; ++i)
        {
            oldColors.Add(renderers[i].material.color);
        }

        mSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            Die();
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
        mSource.PlayOneShot(mClipTakeDamage);
        Debug.Log(mCurrentHealth);
    }

	public override void Die ()
	{
        if(isAlive)
        {
            isAlive = false;
            mSource.PlayOneShot(mClipDie);
            StartCoroutine(explodeWait());
        }
       
	}

    private IEnumerator explodeWait()
    {
        yield return new WaitForSeconds(1.5f);
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        var colliders = GetComponentsInChildren<MeshCollider>();
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            colliders[i].enabled = true;
            rigidbodies[i].useGravity = true;
        }

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
