using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoBehaviour : MonoBehaviour {

	public enum State { Idle, Chase, Hypnotised, Corralled };
	public State state = State.Idle;
	float stateCounter;
	float hypnosisCounter;
	GameObject pen;
    public GameObject goal;
    [Range(0f, 100f)]
    public float viewDistance = 40f;
    NavMeshAgent agent;
	public float CHASE_SPEED;
	public float CORRALLED_SPEED;
	public float IDLE_SPEED;
	public float IDLE_WANDER_RADIUS;
	public float HYPNOTISED_SPEED;
	public float HYPNOSIS_DURATION;

    public PlayerRaycastCheck playerRaycastCheck;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
		stateCounter = 0;
		hypnosisCounter = 0;
    }
    
    // Update is called once per frame
    void Update () {
		if (goal == null) {
			goal = GameObject.Find ("PlayerController(Clone)");
		}
		switch (state) {
		case State.Idle:
			Idle ();
			break;
		case State.Chase:
			Chase ();
			break;
		case State.Hypnotised:
			Hypnotised ();
			break;
		case State.Corralled:
			Corralled ();
			break;
		}
    }

	void Idle() {
		if (CanChaseGoal()) {
			OnChase ();
		} else {
			// Wander to a random position around the dino
			stateCounter -= Time.deltaTime;
			if (stateCounter <= 0) {
				Vector2 wanderTo2 = Random.insideUnitCircle * IDLE_WANDER_RADIUS;
				Vector3 wanderTo3 = new Vector3 (wanderTo2.x, 0, wanderTo2.y);
				agent.destination = transform.position + wanderTo3;
				stateCounter += Random.Range (3f, 9f);
			}
		}
	}

	void OnIdle() {
		state = State.Idle;
		agent.speed = IDLE_SPEED;
		agent.destination = transform.position;
		stateCounter = 0;
	}

	void Chase() {
		if (CanChaseGoal()) {
			agent.destination = goal.transform.position;
		} else {
			OnIdle ();
		}
	}

	void OnChase() {
		agent.destination = goal.transform.position;
		state = State.Chase;
		agent.speed = CHASE_SPEED;
	}

	void Hypnotised() {
		if (hypnosisCounter <= 0) {
			OnIdle ();
		} else {
			hypnosisCounter -= Time.deltaTime;
			agent.destination = goal.transform.position;
		}
	}

	void OnHypnotised(GameObject hypnotiser) {
		goal = hypnotiser;
		agent.speed = HYPNOTISED_SPEED;
		state = State.Hypnotised;
		hypnosisCounter = HYPNOSIS_DURATION;
	}

	void Corralled() {
		// Wander to random position within pen
		stateCounter -= Time.deltaTime;
		if (stateCounter <= 0) {
			Mesh planeMesh = pen.GetComponent<MeshFilter>().mesh;
			Bounds bounds = planeMesh.bounds;

			float dx = pen.transform.localScale.x * bounds.size.x * 0.5f;
			float dz = pen.transform.localScale.z * bounds.size.z * 0.5f;

			Vector3 newVec = new Vector3(
				pen.transform.position.x + Random.Range (-dx, dx),
				pen.transform.position.y,
				pen.transform.position.z + Random.Range (-dz, dz));
			agent.destination = newVec;
			stateCounter += Random.Range (0.5f, 3f);
		}
	}

	void OnCorralled() {
		state = State.Corralled;
		agent.speed = CORRALLED_SPEED;
		stateCounter = 0;
	}

	bool CanChaseGoal() {
		return goal != null && playerRaycastCheck.isChasing && Vector3.Distance (transform.position, goal.transform.position) < viewDistance;
	}

    void OnTriggerEnter(Collider c) {
		if (c.tag == "Pen") {
			OnCorralled ();
			c.GetComponent<DinoPen> ().DinoEnter (this);
			pen = c.gameObject;
		} else if (c.tag == "Player" && state != State.Corralled) {
			PlayerController pc = c.GetComponent<PlayerController> ();
			if (pc != null) {
				pc.DinoEat (this);
			}
		}
    }

	void OnTriggerStay(Collider c) {
		if (c.tag == "Radar" && state != State.Corralled) {
			Debug.Log ("Hypnotised");
			OnHypnotised (c.gameObject);
		}
	}
}
