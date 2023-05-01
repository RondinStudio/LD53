using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitudeLimiter : MonoBehaviour
{
    [SerializeField]
    private float maxMagnitude = 50.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (velocity.magnitude > maxMagnitude)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-velocity);
        }
    }
}
