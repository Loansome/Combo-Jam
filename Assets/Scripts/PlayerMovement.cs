using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterAnimation playerAnimation;
    private Rigidbody playerBody;

    public float walkSpeed = 3f;
    public float zSpeed = 3f;
    
    void Awake()
    {
        playerAnimation = GetComponentInChildren<CharacterAnimation>();
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotatePlayer();
        WalkAnim();
    }

    void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement() // Gets inputs for movement
    {
        playerBody.velocity = new Vector3(
            Input.GetAxisRaw("Horizontal") * (-walkSpeed),
            playerBody.velocity.y,
            Input.GetAxisRaw("Vertical") * (-zSpeed));
    }
    void RotatePlayer() // Rotates player to face where they're moving
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        else if (Input.GetAxisRaw("Horizontal") < 0)
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }
    void WalkAnim()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            playerAnimation.Walk(true);
        else playerAnimation.Walk(false);
    }
}
