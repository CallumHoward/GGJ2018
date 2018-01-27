﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPen : MonoBehaviour {

	HashSet<string> dinoNames;

	// Use this for initialization
	void Start () {
		dinoNames = new HashSet<string> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DinoEnter(DinoBehaviour d) {
		if (!dinoNames.Contains (d.name)) {
			dinoNames.Add (d.name);
			int score = dinoNames.Count;
			Debug.Log ("Score: " + score);
		}
	}

	public void DinoExit(DinoBehaviour d) {
		if (dinoNames.Contains (d.name)) {
			dinoNames.Remove (d.name);
			int score = dinoNames.Count;
			Debug.Log ("Score: " + score);
		}
	}
}
