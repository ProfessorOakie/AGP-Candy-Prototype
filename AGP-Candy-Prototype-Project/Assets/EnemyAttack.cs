using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    protected float mEnemyDamage;
    protected GameObject castle;

    public virtual void Start()
    {
        castle = GameObject.FindGameObjectWithTag("Castle");
    }


}
