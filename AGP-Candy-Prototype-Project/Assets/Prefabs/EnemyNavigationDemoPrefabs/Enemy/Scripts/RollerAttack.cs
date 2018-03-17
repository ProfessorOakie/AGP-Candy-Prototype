using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAttack : MonoBehaviour {

	private void OnCollisionEnter(Collision collision){
		//attack on castle
		if(collision.gameObject.CompareTag("castle")){ 

			//insert attack animation

			//deal damage

			//destroy object
		}
	}


}
