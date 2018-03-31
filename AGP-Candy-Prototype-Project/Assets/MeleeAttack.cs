using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack {

    [SerializeField]
    private float mTimeBetweenAttacks;
    private float attackTimer;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Castle") )
        {
            castle = collision.gameObject;
            attackTimer = mTimeBetweenAttacks;
            castle.GetComponentInParent<CastleHealth>().TakeDamage(mEnemyDamage);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Castle"))
        {
            GetComponent<EnemyMove>().StopNav();
            attackTimer -= Time.deltaTime;
            if(attackTimer<=0)
            {
                if (anim != null)
                    anim.SetTrigger("Attack");
                else
                    Debug.LogWarning("Enemy performing Melee Attack without animation.");
                castle.GetComponentInParent<CastleHealth>().TakeDamage(mEnemyDamage);
                attackTimer = mTimeBetweenAttacks;
            }

        }
        
    }

}
