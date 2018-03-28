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
    private GameObject[] bulletSources;
    private int currBulletSource;

    // Wait time
    [SerializeField]
    [Range(0,5f)]
    [Tooltip("How many seconds between shots when holding down trigger")]
    private float timeBetweenShots;
    private float coolDownTimer = 0;

    //Bullet prefab
    [SerializeField]
    private GameObject skittleBulletPrefab;


    private void Start()
    {
        currBulletSource = 0;
    }

    private void FixedUpdate()
    {
        if (IsTriggerPulled())
        {
            if (coolDownTimer <= 0)
            {
                Fire();
                LoadNextBarrel();
                coolDownTimer = timeBetweenShots;
            }
        }
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
        Vector3 _target = _sourcePos + Vector3.forward * 100f; //temp
        // Pass target to skittlebullet
        _skittleBulletInfo.target = _target;
        Debug.Log("Setting _skittleBulletInfo.target to" + _skittleBulletInfo.target.ToString());
    }

    private void LoadNextBarrel()
    {
        currBulletSource = (currBulletSource + 1) % NUM_BARRELS;
    }
}

//PUT BULLET SPEED IN HERE!
