using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    private CastleHealth castleHealth;
    
	void Start () {

        castleHealth = GetComponent<CastleHealth>();
	}
	

    public void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            Debug.LogWarning("Hard coded damage");
            castleHealth.TakeDamage(1);
        }
    }
}
