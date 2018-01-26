using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Vector3 currentRotation;

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

public class PlayerAnimations
{

    //public Animation anim;
    //public AnimationClip runAnim;
    //public AnimationClip crouchAnim;
    //public AnimationClip crouchIdleAnim;
    //public AnimationClip breathingAnim;

}

public class PlayerController : MonoBehaviour
{
    public PLAYER _PLAYER;
    [MenuItem("Examples/Editor GUILayout Enum Popup usage")]
    static void Init()
    {
        UnityEditor.EditorWindow window = EditorWindow.GetWindow(typeof(PlayerController));
        window.Show();
    }

    public Variables Variables = new Variables();
    public PlayerAnimations PlayerAnimations = new PlayerAnimations();

    


    // Use this for initialization
    void Start()
    {
        
        Cursor.visible = false;
        //Variables.anim = GetComponent<Animation>();
        Variables.player = gameObject.GetComponent<Rigidbody>();
        Variables.colliderPos = GetComponent<CapsuleCollider>().center.y;
        Variables.colliderHeight = GetComponent<CapsuleCollider>().height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float colliderHeightTemp = Variables.colliderHeight / 2f;

        if (Input.GetAxis("Crouch_" + _PLAYER.ToString()) == 1)
        {
            Variables.forwardSpeed = 3f;
            GetComponent<CapsuleCollider>().center = new Vector3(0f, -0.5f, 0f);
            GetComponent<CapsuleCollider>().height = colliderHeightTemp;
        }
        else
        {
            Variables.forwardSpeed = 8f;
            GetComponent<CapsuleCollider>().center = new Vector3(0, Variables.colliderPos, 0);
            GetComponent<CapsuleCollider>().height = Variables.colliderHeight;

        }
        Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal_" + _PLAYER.ToString()), 0f, Input.GetAxisRaw("Vertical_" + _PLAYER.ToString()));
        Direction = Variables.rotationReference.transform.TransformDirection(Direction);
        Direction.y = 0f;
        Vector3 currentPos = Variables.player.transform.position;
        if (Input.GetAxis("Jump_" + _PLAYER.ToString()) == 1 && Variables.jumpAllowed == true)
        {
            if (Variables.grounded == true)
            {
                Variables.grounded = false;
                Variables.player.velocity = (new Vector3(0f, Input.GetAxis("Jump_" + _PLAYER.ToString()) * Variables.jumpForce, 0f));

            }
        }

        if (Input.GetAxis("Sprint_" + _PLAYER.ToString()) == 1f)
        {
            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.sprintSpeed);

        }
        else
        {
            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.forwardSpeed);
        }

        if (Input.GetAxisRaw("Horizontal_" + _PLAYER.ToString()) != 0f || Input.GetAxisRaw("Vertical_" + _PLAYER.ToString()) != 0f)
        {
            //if (Input.GetAxis("Crouch") == 1)
            //{
            //    Variables.anim.clip = Variables.crouchAnim;
            //    Variables.anim["CrouchWalk"].speed = 3;
            //    if (!Variables.anim.IsPlaying(Variables.crouchAnim.name))
            //    {
            //        Variables.anim.CrossFade(Variables.crouchAnim.name, 0.2F, PlayMode.StopAll);
            //    }
            //}
            //else
            //{
            //    Variables.anim.clip = Variables.runAnim;
            //    if (!Variables.anim.IsPlaying(Variables.runAnim.name))
            //    {
            //        Variables.anim.CrossFade(Variables.runAnim.name, 0.2F, PlayMode.StopAll);
            //    }
            //
            //}
            Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal_"+ _PLAYER.ToString()), 0f, Input.GetAxisRaw("Vertical_" + _PLAYER.ToString()));
            Movement = Camera.main.transform.TransformDirection(Movement);
            Movement.y = 0f;
            Variables.childModel.transform.rotation = Quaternion.LookRotation(Movement) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f));
            Variables.currentRotation = Variables.childModel.transform.eulerAngles;
        }
        else
        {
            if (Input.GetAxis("Crouch_" + _PLAYER.ToString()) == 1)
            {
                //Variables.anim.clip = Variables.crouchIdleAnim;
                //if (!Variables.anim.IsPlaying(Variables.crouchIdleAnim.name))
                //{
                //    Variables.anim.CrossFade(Variables.crouchIdleAnim.name, 0.2F, PlayMode.StopAll);
                //}
                Variables.childModel.transform.eulerAngles = Variables.currentRotation;
            }
            else
            {
                //Variables.anim.clip = Variables.breathingAnim;
                //if (!Variables.anim.IsPlaying(Variables.breathingAnim.name))
                //{
                //    Variables.anim.CrossFade(Variables.breathingAnim.name, 0.2F, PlayMode.StopAll);
                //}
                Variables.childModel.transform.eulerAngles = Variables.currentRotation;
            }

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