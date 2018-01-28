using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoBehaviour : MonoBehaviour {

	public enum State { Idle, Spotted, Chase, Hypnotised, Corralled };
	public State state = State.Idle;
	public float stateCounter;
	GameObject pen;
    public GameObject goal;
    [Range(0f, 100f)]
    public float viewDistance = 40f;
    NavMeshAgent agent;
	public EmoteController emote;
	public float ANGULAR_SPEED;
	public float ACCELERATION;
	public float SPOTTED_DURATION;
	public float CHASE_WITHOUT_SEEING_COUNTER;
	public float CHASE_SPEED;
	public float CHASE_ACCELERATION;
	public float SPOTTED_SPEED;
	public float SPOTTED_ANGULAR_SPEED;
	public float CORRALLED_SPEED;
	public float IDLE_SPEED;
	public float IDLE_WANDER_RADIUS;
	public float HYPNOTISED_SPEED;
	public float HYPNOSIS_DURATION;

    [Header("Animation")]
    public Animation anim;
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip attackAnim;
    public AnimationClip spottedAnim;
    public AnimationClip jumpAnim;
    public AnimationClip runAnim;

    public PlayerRaycastCheck playerRaycastCheck;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
		stateCounter = 0;
    }

    // Update is called once per frame
    void Update() {
        if (goal == null) {
            goal = GameObject.Find("PlayerController(Clone)");
        }
        agent.angularSpeed = ANGULAR_SPEED;
		agent.acceleration = ACCELERATION;
        switch (state) {
            case State.Idle:
                Idle();
                break;
			case State.Spotted:
				Spotted ();
				break;
            case State.Chase:
                Chase();
                break;
            case State.Hypnotised:
                Hypnotised();
                break;
            case State.Corralled:
                Corralled();
                break;
        }

        if (state == State.Corralled)
        {
            anim["Walk"].speed = agent.speed * 0.5f;
            anim.clip = walkAnim;
            if (!anim.IsPlaying(walkAnim.name))
            {
                anim.CrossFade(walkAnim.name, 0.2F, PlayMode.StopAll);
            }
        }
        if (state == State.Chase)
        {
            anim["Run"].speed = agent.speed * 0.1875f;
            anim.clip = runAnim;
            if (!anim.IsPlaying(runAnim.name))
            {
                anim.CrossFade(runAnim.name, 0.2F, PlayMode.StopAll);
            }
        }
        if (state == State.Idle || state == State.Hypnotised)
        {
            if (Vector3.Distance(agent.destination, transform.position) >= 0.2f)
            {
                anim["Walk"].speed = agent.speed * 0.5f;
                anim.clip = walkAnim;
                if (!anim.IsPlaying(walkAnim.name))
                {
                    anim.CrossFade(walkAnim.name, 0.2F, PlayMode.StopAll);
                }
            }
            else
            {
                anim.clip = idleAnim;
                if (!anim.IsPlaying(idleAnim.name))
                {
                    anim.CrossFade(idleAnim.name, 0.2F, PlayMode.StopAll);
                }
            }
            
        }
    }

	void Idle() {
		if (CanChaseGoal()) {
			OnSpotted ();
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
		stateCounter = 0;
		emote.Idle ();
	}

	void Spotted() {
		if (goal == null) {
			OnIdle ();
			return;
		}
		agent.angularSpeed = SPOTTED_ANGULAR_SPEED;
		stateCounter -= Time.deltaTime;


		if (stateCounter <= 0) {
			OnChase ();
		}
	}

	void OnSpotted() {
		agent.destination = goal.transform.position;
		agent.speed = SPOTTED_SPEED;
		state = State.Spotted;
		stateCounter = SPOTTED_DURATION;
		emote.Spotted ();

		Debug.Log ("Spotted!");
	}

	void Chase() {
		stateCounter -= Time.deltaTime;
		// TODO: run/chase anim
		
		agent.acceleration = CHASE_ACCELERATION;
		if (goal != null && (stateCounter > 0 || CanChaseGoal())) {
			agent.destination = goal.transform.position;
		} else {
			OnIdle ();
		}
	}

	void OnChase() {
		agent.destination = goal.transform.position;
		state = State.Chase;
		agent.speed = CHASE_SPEED;
		stateCounter = CHASE_WITHOUT_SEEING_COUNTER;
	}

	void Hypnotised() {
		if (stateCounter <= 0) {
			OnIdle ();
		} else {
			stateCounter -= Time.deltaTime;
			agent.destination = goal.transform.position;
			// TODO: run/chase anim
			
		}
	}

	void OnHypnotised(GameObject hypnotiser) {
		goal = hypnotiser;
		agent.speed = HYPNOTISED_SPEED;
		state = State.Hypnotised;
		stateCounter = HYPNOSIS_DURATION;
		emote.Spotted ();
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
			Vector3 dv = newVec - transform.position;
			dv = Vector3.ClampMagnitude (dv, 3);
			agent.destination = transform.position + dv;
			stateCounter += Random.Range (0.5f, 3f);
		}
		
	}

	void OnCorralled() {
		state = State.Corralled;
		agent.speed = CORRALLED_SPEED;
		stateCounter = 0;
		agent.destination = transform.position;
		emote.Captured ();
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
