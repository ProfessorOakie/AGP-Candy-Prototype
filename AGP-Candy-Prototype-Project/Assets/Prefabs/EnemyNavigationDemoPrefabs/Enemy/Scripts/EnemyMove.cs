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

    [SerializeField]
    private float massPickedUpScale = 20.0f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        mRigidbody = GetComponent<Rigidbody>();
        if (agentBlueprint == null) agentBlueprint = agent;
        BeginNav();
	}

    // Enemy was picked up by player
    public void PickedUp()
    {
        agent.SetDestination(transform.localPosition);
        agent.enabled = false;
        mRigidbody.mass *= massPickedUpScale;
    }

    // Player lets go of held enemy
    public void Dropped()
    {
        isFalling = true;
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
    }

    private void Hit()
    {
        agent.SetDestination(transform.localPosition);
        agent.enabled = false;

        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;

        isFalling = true;
    }

    private void BeginNav()
    {
            mRigidbody.isKinematic = true;
            agent.enabled = true;
            if (target)
                agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("ground"))
        {
            Hit();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(isFalling && mRigidbody.velocity.sqrMagnitude == 0)
        {
            isFalling = false;
            BeginNav();
            mRigidbody.mass /= massPickedUpScale;
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
