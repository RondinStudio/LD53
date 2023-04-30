using UnityEngine;

public class Properties : MonoBehaviour
{
    public enum EDirection
    {
        Left = -1,
        Right = 1,
    }

    private EDirection _direction;

    public float Speed;

    // Start is called before the first frame update
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
        surfaceEffector.speed = Speed * (float)_direction;
    }
}
