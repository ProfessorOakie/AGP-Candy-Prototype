using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinigunShootingScript : MonoBehaviour {

    // Static constants
    static int NUM_BARRELS = 8;

    // Shot tracking
    [SerializeField]
    private GameObject rayCastSource;
    [SerializeField]
    private float range;
    [SerializeField]
    private GameObject[] bulletSources;
    private int currBulletSource = 0;

    // Bullet speed
    [SerializeField]
    [Range(0, 50f)]
    private float bulletSpeed;

    // Shot wait time
    [SerializeField]
    [Range(0,5f)]
    [Tooltip("How many seconds between shots when holding down trigger")]
    private float timeBetweenShots;
    private float coolDownTimer = 0;

    //Bullet prefab
    [SerializeField]
    private GameObject skittleBulletPrefab;

    private void FixedUpdate()
    {
        if (IsTriggerPulled())
        {
            // Only allow shot if cooled down, prevent good trigger finger
            if (coolDownTimer <= 0)
            {
                Fire();
                LoadNextBarrel();
                coolDownTimer = timeBetweenShots;
            }
        }
        // Decrement cooldown agnostic to trigger state
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.fixedDeltaTime;
        }
    }

    private bool IsTriggerPulled()
    {
        return true;
    }

    private void Fire()
    {
        // Get initial position
        Vector3 _sourcePos = bulletSources[currBulletSource].transform.position;
        // Spawn in bullet
        GameObject _skittleBullet = (GameObject)Instantiate(skittleBulletPrefab, _sourcePos, Quaternion.identity);
        SkittleBulletScript _skittleBulletInfo = _skittleBullet.GetComponent<SkittleBulletScript>();
        // Raycast to find target
        Vector3 _target = rayCastSource.transform.forward * range;
        RaycastHit _targetInfo;
        if(Physics.Raycast(rayCastSource.transform.position, rayCastSource.transform.forward, out _targetInfo, range))
        {
            _target = _targetInfo.transform.position;
        }
        // Pass target and speed to skittlebullet
        _skittleBulletInfo.bulletSpeed = bulletSpeed;
        _skittleBulletInfo.target = _target;
    }

    private void LoadNextBarrel()
    {
        currBulletSource = (currBulletSource + 1) % NUM_BARRELS;
    }
}