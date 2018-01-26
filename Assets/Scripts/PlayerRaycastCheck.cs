using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastCheck : MonoBehaviour {

	GameObject player;
	public bool isChasing = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerController");
	}

	void Update() {
		gameObject.transform.LookAt(player.transform);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward * 10, Color.green);

		if (Physics.Raycast(transform.position,
				transform.forward,
				out hit,
				Vector3.Distance(transform.position,
				player.transform.position))) {

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
