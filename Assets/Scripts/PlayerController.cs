using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public enum PLAYER
{
    Player_1 = 0,
    Player_2 = 1,
    Player_3 = 2,
    Player_4 = 3
}

[System.Serializable]
public class Variables
{
    [HideInInspector]
    public Rigidbody player;
    [Header("MovementReliant")]
    [Tooltip("Model to rotate when moving")]
    public GameObject childModel;
    [Tooltip("Rotation reference independant of camera position")]
    public GameObject rotationReference;
    public GameObject cameraPosition;
    public GameObject cameraPrefab;
    public Vector3 currentRotation;
	public GameObject radarTransmission;
	public float RADAR_COOLDOWN;
	public float radarCooldownCounter;

    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float sprintSpeed = 15f;
    public float jumpForce = 5f;
    [Range(0, 360)]
    [Tooltip("Rotation in Degrees/s")]
    public float rotateSpeed = 100f;
    public float colliderPos;
    public float colliderHeight;
    public bool jumpAllowed = false;
    public bool grounded = true;

}

[System.Serializable]
public class PlayerAnimations
{

    [Header("Animation")]
    public Animation anim;
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip attackAnim;
    public AnimationClip dieAnim;
    public AnimationClip jumpAnim;
    public AnimationClip runAnim;

}


public class PlayerController : NetworkBehaviour
{
    public PLAYER _PLAYER;
	#if UNITY_EDITOR
    [MenuItem("Examples/Editor GUILayout Enum Popup usage")]
    void Init()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(PlayerController));
        window.Show();
	}
	#endif

    public Variables Variables = new Variables();
    public PlayerAnimations PlayerAnimations = new PlayerAnimations();
    

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        GameObject newCamera = Instantiate(Variables.cameraPrefab, Variables.cameraPosition.transform.position, Variables.cameraPosition.transform.rotation);
        newCamera.transform.parent = gameObject.transform;
        //Variables.anim = GetComponent<Animation>();
        Variables.player = gameObject.GetComponent<Rigidbody>();
        Variables.colliderPos = GetComponent<CapsuleCollider>().center.y;
		Variables.colliderHeight = GetComponent<CapsuleCollider>().height;
		Variables.radarCooldownCounter = 0;
    }

	public void DinoEat(DinoBehaviour d) {
		// Detach camera
		gameObject.GetComponentInChildren<Camera>().transform.parent = null;
		Destroy (gameObject);
		Debug.Log ("Eaten");
		if (FindObjectsOfType<PlayerController> ().Length == 1) {
			// Last player left; end game
			Debug.Log("Game over");
		}
	}

    public Color Test(PLAYER _PLAYER)
    {
        switch (_PLAYER)
        {
            case PLAYER.Player_1:
                return Color.red;
                
            case PLAYER.Player_2:
                return Color.blue;
                
            case PLAYER.Player_3:
                return Color.green;
                
            case PLAYER.Player_4:
                return Color.yellow;
                
        }
        return Color.magenta;
    }











    // Update is called once per frame
    void FixedUpdate()
    {
        /*for (int i = 0; i < NetworkOverride.playerCount - 1; i++)
        {
            NetworkServer.connections[i].playerControllers[0].gameObject.GetComponent<PlayerController>()._PLAYER = (PLAYER)i;
            NetworkServer.connections[i].playerControllers[0].gameObject.GetComponentInChildren<Renderer>().material.color = Test(NetworkServer.connections[i].playerControllers[0].gameObject.GetComponent<PlayerController>()._PLAYER);
        }*/

        float colliderHeightTemp = Variables.colliderHeight / 2f;

        if (!isLocalPlayer)
        {
            return;
        }

		Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal_Player_1"), 0f, Input.GetAxisRaw("Vertical_Player_1")).normalized;
        Direction = Variables.rotationReference.transform.TransformDirection(Direction);
        Direction.y = 0f;
        Vector3 currentPos = Variables.player.transform.position;
        if (Input.GetAxis("Jump_Player_1") == 1 && Variables.jumpAllowed == true)
        {
            if (Variables.grounded == true)
            {
                Variables.grounded = false;
                Variables.player.velocity = (new Vector3(0f, Input.GetAxis("Jump_Player_1") * Variables.jumpForce, 0f));
                PlayerAnimations.anim.clip = PlayerAnimations.jumpAnim;
                PlayerAnimations.anim.CrossFade(PlayerAnimations.jumpAnim.name, 0.2F, PlayMode.StopAll);
            }
        }

        if (Input.GetAxis("Sprint_Player_1") == 1f)
        {
            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.sprintSpeed);
        }
        else
        {
            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.forwardSpeed);
        }

        if (Input.GetAxisRaw("Horizontal_Player_1") != 0f || Input.GetAxisRaw("Vertical_Player_1") != 0f)
        {
            if (Input.GetAxis("Sprint_Player_1") == 1f)
            {
                PlayerAnimations.anim["Run"].speed =  1.5f;
                PlayerAnimations.anim.clip = PlayerAnimations.runAnim;
                if (!PlayerAnimations.anim.IsPlaying(PlayerAnimations.runAnim.name))
                {
                    PlayerAnimations.anim.CrossFade(PlayerAnimations.runAnim.name, 0.2F, PlayMode.StopAll);
                }
            }
            else
            {
                PlayerAnimations.anim["Walk"].speed = 1.5f;
                PlayerAnimations.anim.clip = PlayerAnimations.walkAnim;
                if (!PlayerAnimations.anim.IsPlaying(PlayerAnimations.walkAnim.name))
                {
                    PlayerAnimations.anim.CrossFade(PlayerAnimations.walkAnim.name, 0.2F, PlayMode.StopAll);
                }
            }

            Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal_Player_1"), 0f, Input.GetAxisRaw("Vertical_Player_1"));
            Movement = Camera.main.transform.TransformDirection(Movement);
            Movement.y = 0f;
            Variables.childModel.transform.rotation = Quaternion.LookRotation(Movement) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f));
            Variables.currentRotation = Variables.childModel.transform.eulerAngles;
        }
        else
        {

            PlayerAnimations.anim.clip = PlayerAnimations.idleAnim;
            if (!PlayerAnimations.anim.IsPlaying(PlayerAnimations.idleAnim.name))
            {
                PlayerAnimations.anim.CrossFade(PlayerAnimations.idleAnim.name, 0.2F, PlayMode.StopAll);
            }
            Variables.childModel.transform.eulerAngles = Variables.currentRotation;
            

        }
		Variables.radarCooldownCounter -= Time.deltaTime;
		if (Input.GetAxis ("Jump_Player_1") == 1 && Variables.radarCooldownCounter <= 0) {
			float[] delays = { 0, 0.1f, 0.2f };
			foreach (float delay in delays) {
				GameObject obj = Instantiate (Variables.radarTransmission, gameObject.transform.position, Quaternion.identity);
				obj.GetComponent<RadarController> ().SetDelay (delay);
				obj.transform.parent = transform;
			}
			GetComponent<AudioSource> ().Play ();
			Variables.radarCooldownCounter = Variables.RADAR_COOLDOWN;
		}
    }

    //Collision Detection

    void OnCollisionStay(Collision col)
    {
        Variables.grounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        Variables.grounded = false;
    }
}