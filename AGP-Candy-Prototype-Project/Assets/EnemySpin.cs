using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpin : MonoBehaviour {

    private Vector3 lastXZ;
    private Vector3 currentXZ;

    private float enemyRadius;
    private float enemyCircumference;

    private float lastAxisRotation;

    [SerializeField]
    private GameObject renderedModel;
    [SerializeField]
    private bool shouldSpin = true;

	// Use this for initialization
	void Start () {
        if(renderedModel)
        {
            lastXZ = transform.position;
            currentXZ = transform.position;

            lastAxisRotation = transform.rotation.y;

            enemyRadius = GetComponentInChildren<Renderer>().bounds.extents.y;
            //Debug.Log(enemyRadius);
            enemyCircumference = enemyRadius * 2 * Mathf.PI;
        }
        else 
        {
            shouldSpin = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (shouldSpin)
        {
            // whatever distance it has moved, find out the portion of the circumference that this accounts for, and rotate the object on X by that ratio * 360 degrees.
            lastXZ = currentXZ;
            currentXZ = transform.position;
            float distance = Mathf.Sqrt(Mathf.Pow(currentXZ.x - lastXZ.x, 2) + Mathf.Pow(currentXZ.z - lastXZ.z, 2));
            float portion = distance / enemyCircumference;
            renderedModel.transform.Rotate(0, lastAxisRotation + portion * 360, 0);
        }
	}

    public void setSpinning(bool verdict)
    {
        shouldSpin = verdict;
    }
}
