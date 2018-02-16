using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    public int totalhealth = 100;
    private int healthpoints;
    // Below is bad because should belong to enemy (damage dealt)
    public int damagetaken = 1;

	// Use this for initialization
	void Start () {
        healthpoints = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            float darkness = gameObject.GetComponent<MeshRenderer>().material.color.grayscale;
            if (healthpoints <= 0)
                Destroy(gameObject);
            healthpoints -= damagetaken;
            float grayNow = (float)healthpoints / (float)totalhealth;
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(grayNow, grayNow, grayNow);
        }
    }
}
