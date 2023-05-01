using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootedSawController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject particlesSaw;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 shootDirection)
    {
        rb.velocity = shootDirection * speed;
    }

    private void CheckIfNeedDestroy(Collider2D collision)
    {
        if (!collision.CompareTag("SmallSaw"))
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(-90f, 0f, 0f);
            Instantiate(particlesSaw, transform.position, rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfNeedDestroy(collision);
    }

    private void OnCollisonEnter2D(Collider2D collision)
    {
        CheckIfNeedDestroy(collision);
    }
}
