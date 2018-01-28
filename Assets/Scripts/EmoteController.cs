using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteController : MonoBehaviour {

	public Material SpottedMat;
	public Material CapturedMat;
	public Camera Camera;
	float emoteStartTime;
	public float EMOTE_ANIM_DURATION;
	bool show;
	string emote;
	public AudioClip alertAudio;
	public AudioClip captureAudio;

	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = Vector3.zero;
		show = false;
		emoteStartTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!show) {
			return;
		}
		if (Camera == null) {
			Camera = Camera.main;
		} else {
			gameObject.transform.LookAt (2*transform.position - Camera.transform.position, Vector3.up);
		}
		float scale = easeOutBounce (0, 3f, (Time.time - emoteStartTime) / EMOTE_ANIM_DURATION);
		gameObject.transform.localScale = new Vector3 (scale, scale, scale);
	}

	float easeOutBounce(float start, float end, float value)
	{
		value = Mathf.Clamp01 (value);
		end -= start;

		float d = 1f;
		float p = d * .3f;
		float s = 0;
		float a = 0;

		if (value == 0) return start;

		if ((value /= d) == 1) return start + end;

		if (a == 0f || a < Mathf.Abs(end))
		{
			a = end;
			s = p * 0.25f;
		}
		else
		{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}

		return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
	}

	public void Spotted() {
		show = true;
		if (emote != "spotted") {
			emote = "spotted";
			gameObject.GetComponent<Renderer> ().material = SpottedMat;
			emoteStartTime = Time.time;
			GetComponent<AudioSource> ().PlayOneShot (alertAudio);
		}
	}

	public void Captured() {
		show = true;
		if (emote != "captured") {
			emote = "captured";
			gameObject.GetComponent<Renderer> ().material = CapturedMat;
			emoteStartTime = Time.time;
			GetComponent<AudioSource> ().PlayOneShot (captureAudio);
		}
	}

	public void Idle() {
		show = false;
		gameObject.transform.localScale = Vector3.zero;
	}
}
