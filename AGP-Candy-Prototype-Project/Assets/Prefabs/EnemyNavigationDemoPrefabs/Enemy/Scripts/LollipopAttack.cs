using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopAttack : MonoBehaviour {

	public Transform fire;
	public Rigidbody shot;

	void Start(){
		InvokeRepeating ("Attack", 2.0f, 1.0f);
	}
	private void Attack(){
		//eventually add in a check for how close to castle the enemy is and then enable firing
		Rigidbody rb= Instantiate(shot, fire.position, Quaternion.identity) as Rigidbody;
		rb.velocity = fire.TransformDirection (Vector3.forward * 20);
	}
}
