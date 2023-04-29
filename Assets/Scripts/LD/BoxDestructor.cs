using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestructor : MonoBehaviour
{
    private ScoreController scoreController;

    private void Start()
    {
        scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            // Check la couleur ici plus tard
            Destroy(collision.gameObject);
            scoreController.AddScore(EScoreType.Box);
        }
    }
}
