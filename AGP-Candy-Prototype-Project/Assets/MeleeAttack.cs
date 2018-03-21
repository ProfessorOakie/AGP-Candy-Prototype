using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack {

    [SerializeField]
    private float mTimeBetweenAttacks;
    private float attackTimer;



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Castle") )
        {
            attackTimer = mTimeBetweenAttacks;
            castle.GetComponent<CastleHealth>().TakeDamage(mEnemyDamage);
            GetComponent<EnemyMove>().StopNav();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Castle"))
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer<=0)
            {
                castle.GetComponent<CastleHealth>().TakeDamage(mEnemyDamage);
                attackTimer = mTimeBetweenAttacks;
            }

        }
        
    }

}
