using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {

	public float TRANSMIT_DURATION;
	float transmitCounter;

	// Use this for initialization
	void Start () {
		transmitCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transmitCounter -= Time.deltaTime;
		if (transmitCounter <= 0) {
			gameObject.SetActive (false);
		}
	}

public void Transmit() {
		Debug.Log ("transmitting");
		gameObject.SetActive (true);
	}
}
