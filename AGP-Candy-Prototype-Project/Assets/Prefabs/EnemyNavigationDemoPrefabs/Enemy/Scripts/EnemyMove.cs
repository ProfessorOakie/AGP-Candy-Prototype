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
    private AudioSource mAudioSource;

    // .wav file we want this enemy to play
    public AudioClip mPickedUp;
    public AudioClip mDropped;
    public AudioClip mHit;

    // do not delete audioSource or audioClip between levels -- just in case
    void Awake()
    {
        DontDestroyOnLoad(mAudioSource);
        DontDestroyOnLoad(mPickedUp);
        DontDestroyOnLoad(mDropped);
        DontDestroyOnLoad(mHit);
    }
    private int MaxEnemyHitByEnemyDamage = 30;

    //private Animator mAnimator;

    //[SerializeField]
    //private float massPickedUpScale = 20.0f;

    // Use this for initialization
    void Start () {

        // get the audioSource from the Enemy prefab
        mAudioSource = GetComponent<AudioSource>();

        agent = GetComponent<NavMeshAgent>();
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;

        if (target == null) target = FindObjectOfType<CastleHealth>().GetAttackPoint();

        //mAnimator = GetComponent<Animator>();
        //mAnimator.SetBool("isJumping", true);
        if (agentBlueprint == null) agentBlueprint = agent;
        BeginNav();
	}
		
    // Enemy was picked up by player
    public void PickedUp()
    {
        // play designated audio clip
        if(mAudioSource && mPickedUp)
            mAudioSource.PlayOneShot(mPickedUp, 0.5f);

        agent.enabled = false;
        //mRigidbody.mass *= massPickedUpScale;
        //mAnimator.SetBool("isJumping", false);
        gameObject.layer = LayerMask.NameToLayer("Weapon");
    }

    // Player lets go of held enemy
    public void Dropped()
    {
        if (mAudioSource && mDropped)
            mAudioSource.PlayOneShot(mDropped, 0.5f);

        isFalling = true;
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
    }

    private void Hit(Collision collision)
    {
        if (mAudioSource && mHit)
            mAudioSource.PlayOneShot(mHit, 0.5f);

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
        //mRigidbody.mass /= massPickedUpScale;
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
            GetComponent<Health>().TakeDamage(Mathf.Min(collision.relativeVelocity.magnitude, MaxEnemyHitByEnemyDamage));
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
