﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoBehaviour : MonoBehaviour {

    public bool isCorralled = false;
    public Transform goal;
    NavMeshAgent agent;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void Update () {
        if (!isCorralled) {
            agent.destination = goal.position;
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