using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	Animation anim;
	public float AnimationSpeed;
	bool open;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		anim ["LatchOpen"].layer = 1;
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("g")) {
			PlayAnimation ("LatchOpen", open);
			PlayAnimation ("GateOpen", open);
			open = !open;
		}
	}

	void PlayAnimation(string name, bool reverse) {
		anim.clip = anim.GetClip(name);
		anim [name].speed = AnimationSpeed * (reverse ? -1f : 1f);
		if (reverse && anim [name].time == 0) {
			anim [name].time = anim [name].length;
		}
		anim.Play ();
	}
}
