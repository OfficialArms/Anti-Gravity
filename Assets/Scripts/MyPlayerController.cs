using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private Vector2 velocity;

    [Header("Constants")]
    [SerializeField] private int GRAVITY_SCALE = 6;
    [SerializeField] private float FRICTION = 0.95f;
    [SerializeField] private float X_ACCELERATION = 0.5f;
    [SerializeField] private float JUMP_MODIFIER = 20f;
    [SerializeField] private float MAX_X_SPEED = 10f;
    [SerializeField] private float MAX_Y_SPEED = 40f;
    //[SerializeField] private float
    //[SerializeField] private float
    //[SerializeField] private float

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool holdingJump = false;

    // Wall Sliding
    bool isTouchingFront;
    bool isTouchingBack;
    public Transform frontCheck;
    public Transform backCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    // Wall Jumping
    bool wallJumping;
    public float xWallForce = 15f;
    public float yWallForce = 30f;
    public float wallJumpTime = 0.05f;

    // Ground Terrain
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.gravityScale = GRAVITY_SCALE;
        rigidBody.freezeRotation = true;
        jumpableGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");

        // Deal with the x-axis movement
        if (xInput != 0)
        {
            float newXVelocity = xInput * X_ACCELERATION + rigidBody.velocity.x;
            // Make sure to cap the max speed
            newXVelocity = newXVelocity > MAX_X_SPEED ? MAX_X_SPEED : newXVelocity;
            newXVelocity = newXVelocity < -MAX_X_SPEED ? -MAX_X_SPEED : newXVelocity;

            rigidBody.velocity = new Vector2(newXVelocity, rigidBody.velocity.y);
        }
        else
        {
            // Add Arial Drag if no horizontal movement is applied
            rigidBody.velocity *= new Vector2(FRICTION, 1);
        }

        // JUMP CONTROL
        // Make it so you can hold jumps to keep doing them
        if (Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded() && !holdingJump)
            {   
                // Add the velocity
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, JUMP_MODIFIER * getUpVector().y);

            }
            holdingJump = true;
        }
        else
        {
            holdingJump = false;
        }

        // Cap Y Speed
        if (rigidBody.velocity.y > MAX_Y_SPEED)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, MAX_Y_SPEED);
        }
        else if (rigidBody.velocity.y < -MAX_Y_SPEED)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -MAX_Y_SPEED);
        }

        // Wall Sliding

        // Check if character is against an object
        isTouchingFront = Physics2D.OverlapBox(frontCheck.position, transform.localScale / 2, jumpableGround);
        isTouchingBack = Physics2D.OverlapBox(backCheck.position, transform.localScale / 2, jumpableGround);

        if ((isTouchingFront == true || isTouchingBack == true) && IsGrounded() == false && xInput != 0 && rigidBody.velocity.y < 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if(wallSliding == true)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        // Wall Jumping

        if ((Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping == true)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JUMP_MODIFIER * getUpVector().y);
        }

            // For debugging
            velocity = rigidBody.velocity;
    }

    private Vector2 getUpVector()
    {
        // Get the up vector
        Vector2 upVector = new Vector2(0, rigidBody.gravityScale);
        upVector.Normalize();
        return upVector;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    // Ground Detection
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, getUpVector() * -1, 0.1f, jumpableGround);
    }
    
}



