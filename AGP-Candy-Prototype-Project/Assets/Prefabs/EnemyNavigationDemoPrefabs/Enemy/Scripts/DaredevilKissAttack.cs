using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaredevilKissAttack : MonoBehaviour {

	public Transform[] spawnPoints;
	public Rigidbody explosion;




	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Attack ();
		}

	}

	private void Attack(){
		//create explosion and destroy current daredevil
		for (int i = 0; i < spawnPoints.Length; i++) {
			//spawn explosion prefab at each spawnpoint
			Rigidbody clone=Instantiate(explosion, spawnPoints[i].position, spawnPoints[i].rotation) as Rigidbody;
			if (i % 2 == 0) {
				//spawn forward
				clone.velocity = transform.TransformDirection (Vector3.forward * -10);
			} else {
				//spawn backwards
				clone.velocity = transform.TransformDirection (Vector3.forward * 10);
			}
		}
		Destroy (gameObject);
	}
		
}
