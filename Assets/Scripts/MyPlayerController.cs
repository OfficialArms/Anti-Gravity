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

    // Ground Detection
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, getUpVector() * -1, 0.1f, jumpableGround);
    }
    
}



