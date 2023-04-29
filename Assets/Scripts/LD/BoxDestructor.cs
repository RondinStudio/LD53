using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestructor : MonoBehaviour
{
    private ScoreController scoreController;
    private GoalController goalController;
    private GrabCollisionDetector grabCollisionDetector;

    private void Start()
    {
        scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
        goalController = gameObject.GetComponentInParent<GoalController>();
        grabCollisionDetector = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GrabCollisionDetector>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            BoxValues boxValues = collision.transform.GetComponent<BoxController>().boxValues;
            if (goalController.goalValues.color == boxValues.color)
            {
                scoreController.AddScore(boxValues.scoreValue);
            }
            else
            {
                scoreController.AddScore(-1);
            }
            grabCollisionDetector.DestroyJointIfPresent();
            Destroy(collision.gameObject);
        }
    }
}
