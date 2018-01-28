using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class musicplayer : MonoBehaviour {

	public AudioClip menuMusic;
	public AudioClip exploreMusic;
	public AudioClip chaseMusic;
	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public AudioListener menuAudioListener;
	public NetworkIdentity identifier;
	HashSet<string> dinoNames = new HashSet<string> ();

	// Use this for initialization
	void Start () {
		audioSource1.clip = menuMusic;
		audioSource1.Play ();
		audioSource1.volume = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (identifier.gameObject.activeSelf) {
			if (dinoNames.Count == 0) {
				StartExplore ();
			} else {
				StartChase ();
			}
			menuAudioListener.enabled = false;
		}
	}

	public void AddDino(string name) {
		dinoNames.Add (name);
		//StartChase ();
		Debug.Log ("Add " + name);
	}

	public void RemoveDino(string name) {
		dinoNames.Remove (name);
		/*if (dinoNames.Count == 0) {
			StartExplore ();
		}*/
		Debug.Log ("Remove " + name);
		Debug.Log (dinoNames.Count + " chasing");
	}

	public void StartExplore() {
		/*if (audioSource1.clip.name != exploreMusic.name) {
			audioSource1.clip = exploreMusic;
			audioSource1.Play ();
		}
		audioSource1.volume = 1;
		if (audioSource2.clip.name != chaseMusic.name) {
			audioSource2.clip = chaseMusic;
			audioSource2.Play ();
		}
		audioSource2.volume = 0;*/
		if (audioSource1.clip.name != exploreMusic.name) {
			audioSource1.clip = exploreMusic;
			audioSource1.Play ();
		}
	}

	public void StartChase() {
		/*if (audioSource1.clip.name != exploreMusic.name) {
			audioSource1.clip = exploreMusic;
			audioSource1.Play ();
		}
		audioSource1.volume = 0;
		if (audioSource2.clip.name != chaseMusic.name) {
			audioSource2.clip = chaseMusic;
			audioSource2.Play ();
		}
		audioSource2.volume = 1;*/
		if (audioSource1.clip.name != chaseMusic.name) {
			audioSource1.clip = chaseMusic;
			audioSource1.Play ();
		}
	}
}
