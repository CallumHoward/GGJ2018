using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {

	public float TRANSMIT_DURATION;
	float transmitCounter;
	public float COOLDOWN;
	float cooldownCounter;

	// Use this for initialization
	void Start () {
		transmitCounter = 0;
		cooldownCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transmitCounter -= Time.deltaTime;
		if (transmitCounter <= 0) {
			gameObject.SetActive (false);
		}
		cooldownCounter -= Time.deltaTime;
	}

	public void Transmit() {
		if (cooldownCounter <= 0) {
			Debug.Log ("transmitting");
			gameObject.SetActive (true);
			cooldownCounter = COOLDOWN;
		}
	}
}
