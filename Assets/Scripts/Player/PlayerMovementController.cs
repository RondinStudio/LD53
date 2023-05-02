using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed;                       // Player speed multiplicator

    private Rigidbody2D rb;                    // Reference on the player's rigidbody     

    [SerializeField]
    private float maxRotation = 30;            // Maximum rotation following speed

    [SerializeField]
    private Rigidbody2D chainRb;               // Reference on the chain's rigidbody

    [SerializeField]
    private Vector2 chainOffset;               // chain's offsetTransform

    private float mx;                          // Player x input
    private float my;                          // Player y input
    private Vector2 moveVelocity;              // Player's deplacement vector2
    private Vector2 moveInput;                 // Input Vector2

    [SerializeField]
    private Transform spriteTransform;         // Reference on the player's spriteRenderer transform
    private float velocityInterpolation;
    private float wantedRotation;
    private Quaternion angleQuaternion = Quaternion.identity;

    [SerializeField]
    private AudioSource droneSound;

    void Start()
    {
        // Get all references
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        velocityInterpolation = 0;

        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(mx, my);
        moveVelocity = moveInput.normalized * speed;

        Debug.Log(Time.timeSinceLevelLoad);
    }

    // Full version, clamping settable on all 4 range elements (in1, in2, out1, out2)
    private float remap(float val, float in1, float in2, float out1, float out2,
        bool in1Clamped, bool in2Clamped, bool out1Clamped, bool out2Clamped)
    {
        if (in1Clamped == true && val < in1) val = in1;
        if (in2Clamped == true && val > in2) val = in2;

        float result = out1 + (val - in1) * (out2 - out1) / (in2 - in1);

        if (out1Clamped == true && result < out1) result = out1;
        if (out2Clamped == true && result > out2) result = out2;

        return result;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVelocity * Time.fixedDeltaTime);

        if (rb.velocity.sqrMagnitude > 10)
        {
            if (rb.velocity.y != 0)
            {
                velocityInterpolation = rb.velocity.x / Mathf.Abs(rb.velocity.y);
                wantedRotation = remap(-velocityInterpolation, -1, 1, -maxRotation, maxRotation, true, true, true, true);
            }

            spriteTransform.eulerAngles = new Vector3(0, 0, wantedRotation);
        }
        else
        {
            spriteTransform.eulerAngles = new Vector3(0, 0, 0);
        }

        droneSound.volume = 0.01f * rb.velocity.magnitude;
    }
}