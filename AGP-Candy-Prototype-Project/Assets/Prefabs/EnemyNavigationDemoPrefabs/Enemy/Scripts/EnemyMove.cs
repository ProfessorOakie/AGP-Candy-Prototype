using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (target)
            agent.destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
