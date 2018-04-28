using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Public variables
    public float speed = 6f;

    // Private variables
    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerBody;
    private int floorMask;
    private float rayLength = 100f;

    // Setup references
    void Awake()
    {
    	floorMask = LayerMask.GetMask("Floor");
    	animator = GetComponent<Animator>();
    	playerBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
    	float h = Input.GetAxisRaw("Horizontal"); // a or d, left arrow or right arrow
        float v = Input.GetAxisRaw("Vertical"); // w or s, up arrow or down arrow

        Move(h, v);
        Turn();
        Animate(h, v);
    }

    /**
     * Move the player.
     */
    void Move(float h, float v)
    {
        // x and z are float on the ground. No vertical movement.
        movement.Set(h, 0f, v);

        // Length of h and v is 1, but h AND v (diagonal) is 1.4, so normalize that.
        movement = movement.normalized * speed * Time.deltaTime;

        // Apply movement to the player
        playerBody.MovePosition(transform.position + movement);
    }

    /**
     * Turn the player.
     */
    void Turn()
    {
        // Create a ray that casts from the camera, through the screen, to the floor quad
        // This will return a particular position where the mouse cursor is.
        // We want the player to turn when the mouse cursor is.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Information we need to return. 
        RaycastHit floorHit;

        // Perform action of casting the ray to hit something.
        // If something is hit, return true.
        if(Physics.Raycast(camRay, out floorHit, rayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f; // Make sure y is zero so player stays straight up.

            // Quaternion used to store rotation (cant use Vector3)
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerBody.MoveRotation(newRotation);
        }
    }

    void Animate(float h, float v)
    {
        // Did we press horizontal or vertical axis movement.
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }
}
