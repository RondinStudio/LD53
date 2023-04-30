using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireRotation : MonoBehaviour
{
    public float speed = 180;

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime); 
    }
}
