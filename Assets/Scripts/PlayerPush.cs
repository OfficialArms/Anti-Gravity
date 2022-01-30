using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{

    public float MOVEABLE_DISTANCE = 1f;
    public LayerMask boxLayer;

    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // So the player doesn't push themselves
        Physics2D.queriesStartInColliders = false;

        // Local scale makes sure the x is based on which way they are looking
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, MOVEABLE_DISTANCE, boxLayer);

        if(hit.collider != null && hit.collider.gameObject.tag == "Grabable" && Input.GetKeyDown(KeyCode.E))
        {
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;

            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, (Vector2) transform.position + Vector2.right * transform.localScale.x * MOVEABLE_DISTANCE);
    }
}
