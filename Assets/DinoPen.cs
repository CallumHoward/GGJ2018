using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Raptor") {
			Debug.Log ("Got a raptor");
		}
	}
}
