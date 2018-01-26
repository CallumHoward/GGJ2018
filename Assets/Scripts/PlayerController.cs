using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody PlayerObject;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		Vector3 Direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
		Direction = Camera.main.transform.TransformDirection (Direction);
		Direction.y = 0f;
		gameObject.transform.position = (transform.position + Direction * Time.deltaTime);
	}
}