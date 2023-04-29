using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed;                       // Player speed multiplicator

    private Rigidbody2D rb;                    // Reference on the player's rigidbody

    [SerializeField]
    private Rigidbody2D chainRb;               // Reference on the chain's rigidbody

    [SerializeField]
    private Vector2 chainOffset;               // chain's offsetTransform

    private float mx;                          // Player x input
    private float my;                          // Player y input
    private Vector2 moveVelocity;              // Player's deplacement vector2
    private Vector2 moveInput;                 // Input Vector2

    void Start()
    {
        // Get all references
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        if (mx == 0 && my == 0)
        {
            // Set movement variable to false in the animator once we have it
        }
        else
        {
            // Set movement variable to true in the animator once we have it
        }

        moveInput = new Vector2(mx, my);
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVelocity * Time.fixedDeltaTime);
        //chainRb.position = rb.position + chainOffset;
    }
}
