using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPen : MonoBehaviour {

	int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DinoEnter(DinoBehaviour d) {
		score++;
		Debug.Log ("Score: " + score);
	}

	public void DinoExit(DinoBehaviour d) {
		score--;
		Debug.Log ("Score: " + score);
	}
}
