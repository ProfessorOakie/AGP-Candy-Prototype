using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this should be used to help manage combining characters in the scene

public class CombineAbility : MonoBehaviour {
	private bool canCombine;
	private Collider c;
	public GameObject combinedEnemy;
	// Use this for initialization
	void Start () {
		canCombine = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)&& canCombine) {
			Combine (c);
		}
	}
	//When two daredevils are next to each other press D to combine them
	private void OnTriggerEnter(Collider other){
		//if tags are the same then combine 
		//need diff logic for combining two different enemies
		if (other.gameObject.CompareTag (this.tag)) {
			//delete both of them and scale a new instance up so it looks like they combined
			//we can add other conditions here so that they only combine after a certian time, number of waves, ect.
			canCombine=true;
			c = other;
		}
	}
	private void Combine(Collider other){
		//delete both of them and scale a new instance up so it looks like they combined
		//we can add other conditions here so that they only combine after a certian time, number of waves, ect.
		Transform temp= other.transform;
		Destroy(gameObject);
		Destroy (other.gameObject);
		GameObject daredevil = Instantiate (combinedEnemy, temp.position, Quaternion.identity) as GameObject;
		daredevil.transform.localScale += new Vector3 (4,4,4);

	}
}
