using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour {

	public GameObject gate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider c) {
		if (c.tag == "Player") {
			gate.GetComponent<GateController> ().OpenGate ();
		}
	}
}
