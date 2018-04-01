using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    protected float mCurrentHealth;

    [SerializeField]
    protected float mMaxHealth = 100;

    protected virtual void Start()
    {
        mCurrentHealth = mMaxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        //Debug.Log(gameObject.name + gameObject.GetInstanceID() + " :: damage: " + amount + " :: time: " + Time.time);
        mCurrentHealth -= amount;
        if (mCurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void AddHealth(float amount)
    {
        mCurrentHealth += amount;
        mCurrentHealth = Mathf.Min(mCurrentHealth, mMaxHealth);
    }
    

}
