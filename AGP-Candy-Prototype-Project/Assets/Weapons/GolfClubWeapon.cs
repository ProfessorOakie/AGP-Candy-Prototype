using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClubWeapon : Weapon
{

    private AudioSource mAudioSource;


    private Vector3 mPrevPosition = Vector3.zero;

    private Vector3 mVelocity = Vector3.zero;

    private bool mAudioPlaying;

    //this is the damage scale of the "Sweet Spot"
    [SerializeField]
    private float mClubDamageScale;
    private SphereCollider clubCollider;

    // TODO
    //public AudioClip mSwishAudio;
    // when mVelocity > mSwishThreshold, play swish audio
    //public float mSwishThreshold;

    // .wav file we want played on collision
    private AudioClip mAudioClip;

    // do not delete audioSource or audioClip between levels -- just in case
    void Awake()
    {
        DontDestroyOnLoad(mAudioSource);
        DontDestroyOnLoad(mAudioClip);
    }

    void Update()
    {
        // TODO
        /*
        Vector3 tempPos = GetComponent<Rigidbody>().transform.position;
        mVelocity =  tempPos - mPrevPosition;
        mPrevPosition = tempPos;

        mVelocity /= Time.fixedDeltaTime;

        if(mVelocity.magnitude >= mSwishThreshold && !mAudioPlaying)
        {
            mAudioSource.clip = mSwishAudio;
            mAudioSource.Play();
            mAudioSource.loop = true;
            mAudioPlaying = true;

        } else if(mVelocity.magnitude < mSwishThreshold)
        {
            mAudioSource.Stop();
            mAudioPlaying = false;
        }

    */


    }


    // Use this for initialization
    protected override void Start()
    {
        mAudioSource = GetComponent<AudioSource>();

        clubCollider = GetComponent<SphereCollider>();
        base.Start();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject;
        var enemyHealth = enemy.GetComponent<EnemyHealth>();

        Debug.LogWarning("TODO: cleanup this polymorphism");

        if (enemyHealth && (collision.contacts[0].thisCollider.Equals(clubCollider)))
        {

            // play collision sound
            mAudioSource.PlayOneShot(mAudioClip, 0.5f);

            float damage = (collision.relativeVelocity.magnitude) * mClubDamageScale;
            enemyHealth.TakeDamage(damage);
            GetComponent<WeaponHealth>().TakeDamage(damage);

            if (interactableItem.AttachedHand != null)
            {
                rumbling = true;
                interactableItem.AttachedHand.TriggerHapticPulse(1000, NewtonVR.NVRButtons.Touchpad);
            }


        }
        else
        {
            mAudioSource.PlayOneShot(mAudioClip, 0.3f);
            base.OnCollisionEnter(collision);
        }

    }




}
