using System;
using UnityEngine;

public class Properties : MonoBehaviour
{

    public enum EDirection
    {
        Left = -1,
        Right = 1,
    }

    public EDirection Direction;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        var surfaceEffector = gameObject.GetComponent<SurfaceEffector2D>();
        surfaceEffector.speed = Speed*(float)Direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
