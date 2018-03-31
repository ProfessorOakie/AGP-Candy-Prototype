using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    private CastleHealth castleHealth;
    
	void Start () {

        castleHealth = GetComponent<CastleHealth>();
	}
	
    //TODO this won't work using the "AttackPoint" thing which is a collider in a different part of the object
    public void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //Removed by Zach
            /*
            Destroy(other.gameObject);
            Debug.LogWarning("Hard coded damage");
            castleHealth.TakeDamage(1);
            */
        }
    }
}
