using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private const int GRAVITY_SCALE = 10;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.gravityScale = GRAVITY_SCALE;
        rigidBody.freezeRotation = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }


    // Hit box Detection
    private void OnTriggerEnter2D(Collider2D col)
    {

        switch (col.gameObject.tag)
        {
            case "GravityPadActivation" when rigidBody.gravityScale > 0:
                InvertGravity();
                break;
            case "GravityPadDeactivation" when rigidBody.gravityScale < 0:
                InvertGravity();
                break;

        }
    }

    // Inverts current Gravity
    public void InvertGravity()
    {
        rigidBody.gravityScale *= -1;
    }
}
