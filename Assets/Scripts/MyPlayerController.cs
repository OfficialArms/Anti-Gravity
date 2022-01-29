using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private bool _gravityInverted = false;
    [SerializeField] private const int GRAVITY_SCALE = 10;
    [SerializeField] private Vector2 velocity;


    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private Vector2 upVector;

    // Ground Terrain
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        rigidBody.gravityScale = GRAVITY_SCALE;
        rigidBody.freezeRotation = true;
        // By default the up vector will be up
        upVector = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0)
        {
            rigidBody.velocity = new Vector2(xInput * 7f, rigidBody.velocity.y);
        }
        if (Input.GetButton("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 20f * upVector.y);
        }
        if (Input.GetKeyDown(KeyCode.P)) // Flip Gravity
        {
            _gravityInverted = !_gravityInverted;
            rigidBody.gravityScale *= -1;
            upVector.y *= -1;
        }

        // For debugging
        velocity = rigidBody.velocity;
    }


    // Ground Detection
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, upVector * -1, 1f, jumpableGround);
    }
    
}



