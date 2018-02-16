using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

    public Transform target;
    private NavMeshAgent agent;
    private static NavMeshAgent agentBlueprint;
    private Rigidbody mRigidbody;
    private bool isFalling;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        if (agentBlueprint == null) agentBlueprint = agent;
        if (target)
            agent.SetDestination(target.position);
        mRigidbody = GetComponent<Rigidbody>();
	}

    // Enemy was picked up by player
    public void PickedUp()
    {
        agent.SetDestination(transform.localPosition);
        agent.enabled = false;
    }

    // Player lets go of held enemy
    public void Dropped()
    {
        isFalling = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(isFalling && mRigidbody.velocity.sqrMagnitude == 0)
        {
            isFalling = false;
            mRigidbody.isKinematic = true;
            //agent = gameObject.AddComponent<NavMeshAgent>();
            //agent = agentBlueprint;
            agent.enabled = true;
            agent.SetDestination(target.position);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("reset");
                //agent.ResetPath();
                //agent.SetDestination(target.position);
        }
    }

}
