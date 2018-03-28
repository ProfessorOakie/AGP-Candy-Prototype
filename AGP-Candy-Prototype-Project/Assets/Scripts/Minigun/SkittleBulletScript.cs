using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SkittleBulletScript : MonoBehaviour {

    // Array of colors of classic Skittles as found online
    private static Color[] colors =
    {
        new Color(230f/255f,72f/255f,8f/255f),    // Orange
    	new Color(241f/255f,190f/255f,2f/255f),   // Yellow
    	new Color(4f/255f,130f/255f,7f/255f),     // Green
    	new Color(68f/255f,19f/255f,73f/255f),    // Purple
    	new Color(192f/255f,4f/255f,63f/255f)     // Red
    };

    // Bullet Speed
    [HideInInspector]
    public float bulletSpeed;

    // Destination
    [HideInInspector]
    public Vector3 target = Vector3.zero;

    // Direction
    private bool isDirectionFound = false;
    private Vector3 direction = Vector3.zero;


    // Rigidbody
    private Rigidbody rb;


    void Start () {
        // Initialize values
        rb = GetComponent<Rigidbody>();
        // Set to random skittle color on start
        Renderer _rend = GetComponentInChildren<Renderer>();
        _rend.material.color = colors[Random.Range(0, colors.Length)];
    }

    private void FixedUpdate()
    {
        // Set direction if not found already, but only if target is acquired
        if (!isDirectionFound)
        {
            if (target != Vector3.zero)
            {
                direction = (target - transform.position).normalized;
                isDirectionFound = true;
            }
        }
        // Move along direction otherwise
        else
        {
            rb.MovePosition(transform.position + direction * (bulletSpeed * Time.fixedDeltaTime));
        }
    }


}
