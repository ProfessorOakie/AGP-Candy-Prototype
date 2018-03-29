using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private float period;

	[SerializeField]
	private float verticalShift;

	[SerializeField]
	private float horizontalShift;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform transform = GetComponent<Transform>();
		Vector3 newPos = transform.position;
		newPos.y = period * Mathf.Cos((speed * Time.time) + horizontalShift) + verticalShift;

		transform.position = newPos;
	}
}
