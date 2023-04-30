using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallOfDeathController : MonoBehaviour
{
    private PlayerHealthController playerHealthController;
    private GrabCollisionDetector grabCollisionDetector;

    [SerializeField]
    private AudioSource destructionSound;

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

            if (!destructionSound.isPlaying)
                destructionSound.Play();

            Destroy(collision.gameObject);
            if (playerHealthController)
            {
                playerHealthController.RemoveHealth();
            }
        }
    }
}
