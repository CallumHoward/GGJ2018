using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastCheck : MonoBehaviour {

	GameObject player;
	public bool isChasing = false;

	public DinoBehaviour dinoBehaviour;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerController");
	}

	void Update() {
		gameObject.transform.LookAt(player.transform);
		Mathf.Clamp(transform.rotation.x, -135, -45);
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
