using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	Animation anim;
	public float AnimationSpeed;
	float openCounter;
	public float OPEN_DURATION;
	bool open;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		anim ["LatchOpen"].layer = 1;
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		openCounter -= Time.deltaTime;
		if (openCounter <= 0) {
			CloseGate ();
		}
	}

	public void OpenGate() {
		openCounter = OPEN_DURATION;
		if (open) {
			return;
		}
		PlayAnimation ("LatchOpen", false);
		PlayAnimation ("GateOpen", false);
		open = true;
	}

	void CloseGate() {
		if (!open) {
			return;
		}
		PlayAnimation ("LatchOpen", true);
		PlayAnimation ("GateOpen", true);
		open = false;
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
