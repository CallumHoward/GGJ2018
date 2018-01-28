using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {

	public float TRANSMIT_DURATION;
	float transmitCounter;
	float timeStart;
	public float SCALE_MAX;
	public float SCALE_DURATION;

	// Use this for initialization
	void Start () {
		transmitCounter = 0;
	}

	public void SetDelay(float delay) {
		timeStart = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
		// Interp the scale to max
		float value = (Time.time - timeStart) / SCALE_DURATION;
		if (value >= 1.0f) {
			Destroy (gameObject);
			return;
		}
		gameObject.transform.localScale = Vector3.Slerp (Vector3.zero, Vector3.one * SCALE_MAX, value);
	}
}
