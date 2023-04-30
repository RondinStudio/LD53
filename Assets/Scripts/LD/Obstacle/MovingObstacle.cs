using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private readonly float[] _speeds = { 1.0f, 1.25f, 1.50f, 1.75f, 2.0f };

    public float Speed;

    public List<GameObject> Waypoints;

    private int currentWaypointTarget = 1;

    private Vector3 _targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        Speed *= _speeds[Random.Range(0, _speeds.Length)];
        if (Waypoints.Count >= 2)
        {
            _targetPosition = Waypoints[currentWaypointTarget].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var movingStep = Time.deltaTime * Speed;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, movingStep);

        if(transform.position == _targetPosition)
        {
            currentWaypointTarget = (currentWaypointTarget+1) % Waypoints.Count;
            _targetPosition = Waypoints[currentWaypointTarget].transform.position;
        }
    }
}
