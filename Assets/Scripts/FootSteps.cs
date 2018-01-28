using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour {

    public AudioClip[] footSteps;
    private AudioSource audioSource;
    public bool Big = false;
    public bool Grass = true;

	// Use this for initialization
	void Start () {
	}
    private void OnTriggerEnter(Collider other)
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null) {
			return;
		}
        if (other.tag == "Terrain" && !Big)
        {
			audioSource.clip = footSteps[Random.Range(8, 15)];
            audioSource.Play();
            print("Playing Sound");

        }
        else if (other.tag == "Terrain" && Big)
        {
            audioSource.clip = footSteps[Random.Range(0, 7)];
			audioSource.Play();
            print("Playing Sound");

        }
    }

}
