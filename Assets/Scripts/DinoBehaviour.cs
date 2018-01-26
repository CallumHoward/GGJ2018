using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoBehaviour : MonoBehaviour {

    public bool isCorralled = false;
    public GameObject goal;
    [Range(0f, 100f)]
    public float viewDistance = 40f;
    NavMeshAgent agent;

    public PlayerRaycastCheck playerRaycastCheck;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("PlayerController");
    }
    
    // Update is called once per frame
    void Update () {
        if (!isCorralled && playerRaycastCheck.isChasing && Vector3.Distance(transform.position, goal.transform.position) < viewDistance) {
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
