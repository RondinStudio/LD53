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

    private int numberOfBoxOn = 0;

    void Start()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ArrowController>().AddConveyor(this);

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

        foreach (TireRotation tireRotation in gameObject.GetComponentsInChildren<TireRotation>())
        {
            tireRotation.speed *= -(float)_direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            numberOfBoxOn++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            numberOfBoxOn--;
        }
    }

    public bool HasBoxOn()
    {
        return numberOfBoxOn > 0;
    }
}
