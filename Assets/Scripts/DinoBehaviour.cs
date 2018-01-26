using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoBehaviour : MonoBehaviour {

    public bool isCorralled = false;
    public GameObject goal;
    NavMeshAgent agent;

    public PlayerRaycastCheck playerRaycastCheck;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("PlayerController");
    }
    
    // Update is called once per frame
    void Update () {
        if (!isCorralled && playerRaycastCheck.isChasing) {
            agent.destination = goal.transform.position;
        } else {
            agent.destination = transform.position;
        }
    }

    void OnTriggerStay(Collider c) {
        if (c.tag == "Pen") {
            isCorralled = true;
        }
    }
}
