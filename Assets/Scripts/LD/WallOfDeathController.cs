using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallOfDeathController : MonoBehaviour
{
    private PlayerHealthController playerHealthController;
    private GrabCollisionDetector grabCollisionDetector;

    private void Start()
    {
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        grabCollisionDetector = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GrabCollisionDetector>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            if (grabCollisionDetector.GetConnectedObject() == collision.gameObject)
            {
                grabCollisionDetector.DestroyJointIfPresent();
            }

            Destroy(collision.gameObject);
            if (playerHealthController)
            {
                playerHealthController.RemoveHealth();
            }
        }
    }
}
