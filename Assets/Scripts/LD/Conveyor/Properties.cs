using UnityEngine;

public class Properties : MonoBehaviour
{
    public enum EDirection
    {
        Left = -1,
        Right = 1,
    }

    private readonly float[] _speeds = { 1.0f, 1.25f, 1.50f, 1.75f, 2.0f };

    private EDirection _direction;

    public float Speed;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").transform.position.x < gameObject.transform.position.x)
        {
            _direction = EDirection.Left;
        }
        else
        {
            _direction = EDirection.Right;
        }

        SurfaceEffector2D surfaceEffector = gameObject.GetComponent<SurfaceEffector2D>();
        surfaceEffector.speed = Speed * (float)_direction * _speeds[Random.Range(0, _speeds.Length)];

        foreach (TireRotation tireRotation in gameObject.GetComponentsInChildren<TireRotation>())
        {
            tireRotation.speed *= -(float)_direction;
        }
    }
}
