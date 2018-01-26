using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastCheck : MonoBehaviour {

	public GameObject player;
	public bool isChasing = false;

	public DinoBehaviour dinoBehaviour;

	void Update() {
        if (player != null)
        {
            gameObject.transform.LookAt(player.transform);
        }
        else
        {
            player = GameObject.Find("PlayerController(Clone)");
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward * dinoBehaviour.viewDistance, Color.red);

		if (Physics.Raycast(transform.position, transform.forward, out hit, dinoBehaviour.viewDistance)) {

			if (hit.collider.gameObject.tag == "Player") {
				isChasing = true;
			} else {
				isChasing = false;
			}

		} else {
			isChasing = false;
		}
	}
}
