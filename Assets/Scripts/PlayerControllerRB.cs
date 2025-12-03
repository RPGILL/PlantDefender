using UnityEngine;    // use Unity basic tool and stuff

[RequireComponent(typeof(Rigidbody))]    // this says the GameObject MUST have a Rigidbody attach
public class PlayerControllerRB : MonoBehaviour
{
    [Header("Movement Settings")]    // this header just groups fields in the In
    public float walkSpeed = 3f;        // this normal walking speed
    public float runSpeed = 12f;        // this is running speed when holding Shift
    public float turnSpeed = 10f;       //this id  how quickly bunny turns toward movement
    public float acceleration = 15f;    // this smooth blending for animation speed

    private Rigidbody rb;    // reference to the Rigidbody on this playe
    private Animator animator;   // reference to the Animator on the child object for runwalk animatio
    private float currentAnimSpeed = 0f;     // current animation speed value we send into the Animatr
    private bool isRunning = false;     // this  track Shift key state

    void Awake()   // Awake runs when the object is first created
    {
        // this is Get references
        rb = GetComponent<Rigidbody>();     // get the Rigidbody component on this GameObject and sav
        animator = GetComponentInChildren<Animator>();     // get the Animator from a child object for character animati

        // this  Prevent bunny from tipping over when moving
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;         // stop the player from tipping over on X and Z rotatn
    }

    void Update()      // Update runs every frame (good for reading in
    {
        //b this  DETECT SHIFT IN UPDATE more reliable
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);    // check if left or right Shift is held down right 

        // this Debug to confirm Shift key detection
        Debug.Log("Shift pressed: " + isRunning);     // write to the Console so we can see if Shift detection wo
    }

    void FixedUpdate()       // FixedUpdate runs on a fixed time step good for physics move
    {
        // this  GET MOVEMENT INPUT 
        float h = Input.GetAxis("Horizontal");   // A D or Left Right
        float v = Input.GetAxis("Vertical");     // W S or Up Down

        //  this DETERMINE MOVE DIRECTION and stuff
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        // this INSTANT STOP FIX 
        if (inputDir.sqrMagnitude < 0.001f)
        {
            //this  Stop animation immediately when not moving
            currentAnimSpeed = 0f;
            if (animator != null)
                animator.SetFloat("Speed", 0f);
            return; // Exit early — no movement
        }

        // this  DETERMINE MOVEMENT SPEED 
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        // this  MOVE CHARACTER 
        Vector3 move = inputDir * targetSpeed * Time.fixedDeltaTime;     // turn input into a 3D direction on the XZ plane, then normaliz
        rb.MovePosition(rb.position + move);    // if input direction is almost zero not mov

        // this Rotate smoothly toward movement direction
        Quaternion targetRot = Quaternion.LookRotation(inputDir); // it target the rot 
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);   // this is the rb  target

        // this  HANDLE ANIMATION SPEED BLENDING 
        float targetAnimSpeed = inputDir.magnitude * (isRunning ? 2f : 1f);     /// flaot target speed 
        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, Time.deltaTime * acceleration);      /// this is the curtrenmt speed 

        if (animator != null)      // if we have an animatorand  tell it speed is 
        {    
            animator.SetFloat("Speed", currentAnimSpeed);      // do not move or rotate this fra

            // this  BOOST ANIMATION PLAYBACK WHEN RUNNING 
            if (isRunning && currentAnimSpeed > 1.0f)     // if running, use runSpeed; else use walkSpe
                animator.speed = 2.0f;   // double animation speed while running
            else
                animator.speed = 1.0f;   // normal animation speed otherwise
        }
    }
}
