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
        audioSource = GetComponent<AudioSource>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain" && !Big)
        {
            audioSource.clip = footSteps[Random.Range(8, 15)];
            audioSource.PlayOneShot(footSteps[Random.Range(8, 15)]);
            print("Playing Sound");

        }
        else if (other.tag == "Terrain" && Big)
        {
            audioSource.clip = footSteps[Random.Range(0, 7)];
            audioSource.PlayOneShot(footSteps[Random.Range(0, 7)]);
            print("Playing Sound");

        }
    }

}
