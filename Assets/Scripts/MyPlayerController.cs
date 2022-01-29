using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private bool _gravityInverted = false;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private const int GRAVITY_SCALE = 10;
    [SerializeField] private Vector2 velocity;
    private Vector2 upVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = GRAVITY_SCALE;
        rigidBody.freezeRotation = true;
        // By default the up vector will be up
        upVector = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0)
        {
            rigidBody.velocity = new Vector2(xInput * 7f, rigidBody.velocity.y);
        }
        if (Input.GetButton("Jump"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 20f) * upVector;
        }
        if (Input.GetKeyDown(KeyCode.P)) // Flip Gravity
        {
            _gravityInverted = !_gravityInverted;
            rigidBody.gravityScale *= -1;
            upVector *= new Vector2(1, -1);
        }

        // For debugging
        velocity = rigidBody.velocity;
    }
}
