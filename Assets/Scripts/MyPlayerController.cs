using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private bool _gravityInverted = false;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private const int GRAVITY_SCALE = 10;
    private Vector2 upVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = GRAVITY_SCALE;
        // By default the up vector will be up
        upVector = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");

        if (xInput != 0)
        {
            rigidBody.velocity = new Vector2(xInput * 4f, rigidBody.velocity.y);
        }
        if (Input.GetButton("Jump"))
        {
            rigidBody.velocity = new Vector2(0, 7f) * upVector;
            print(new Vector2(0, 7f) * upVector);
        }
        if (Input.GetKeyDown(KeyCode.P)) // Flip Gravity
        {
            _gravityInverted = !_gravityInverted;
            rigidBody.gravityScale *= -1;
            upVector *= new Vector2(0, -1);
        }
    }
}
