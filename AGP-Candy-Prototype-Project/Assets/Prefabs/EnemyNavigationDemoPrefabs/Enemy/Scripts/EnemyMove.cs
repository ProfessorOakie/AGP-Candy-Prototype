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
    //private Animator mAnimator;

    [SerializeField]
    private float massPickedUpScale = 20.0f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;

        if (target == null) target = FindObjectOfType<CastleHealth>().transform;

        //mAnimator = GetComponent<Animator>();
        //mAnimator.SetBool("isJumping", true);
        if (agentBlueprint == null) agentBlueprint = agent;
        BeginNav();
	}
		
    // Enemy was picked up by player
    public void PickedUp()
    {
        agent.enabled = false;
        mRigidbody.mass *= massPickedUpScale;
        //mAnimator.SetBool("isJumping", false);
        gameObject.layer = LayerMask.NameToLayer("Weapon");
    }

    // Player lets go of held enemy
    public void Dropped()
    {
        isFalling = true;
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
    }

    private void Hit(Collision collision)
    {
        agent.enabled = false;

        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
        
        isFalling = true;
        //mAnimator.SetBool("isJumping", false);
        gameObject.layer = LayerMask.NameToLayer("Weapon");
    }

    private void GetUp()
    {
        isFalling = false;
        BeginNav();
        mRigidbody.mass /= massPickedUpScale;
        //mAnimator.SetBool("isJumping", true);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    private void BeginNav()
    {
            //mRigidbody.isKinematic = true;
            agent.enabled = true;
            if (target)
                agent.SetDestination(target.position);
    }

    public void StopNav()
    {
        agent.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("ground"))
        {
            Hit(collision);
            GetComponent<Health>().TakeDamage(collision.relativeVelocity.magnitude);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(isFalling && mRigidbody.velocity.sqrMagnitude == 0)
        {
            GetUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("ground"))
        {
            if(mRigidbody)
                mRigidbody.isKinematic = false;
        }
    }

}
