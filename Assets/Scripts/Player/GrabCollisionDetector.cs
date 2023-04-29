using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollisionDetector : MonoBehaviour
{
    private FixedJoint2D boxJoint;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            if (!boxJoint)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    // creates joint
                    boxJoint = gameObject.AddComponent<FixedJoint2D>();
                    // sets joint position to point of contact
                    boxJoint.anchor = collision.ClosestPoint(collision.transform.position);
                    // conects the joint to the other object
                    boxJoint.connectedBody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
                    // Stops objects from continuing to collide and creating more joints
                    boxJoint.enableCollision = false;
                }
            }
        }
    }

    void Update()
    {
        if (boxJoint)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(boxJoint);
            }
        }
    }

    public void DestroyJointIfPresent()
    {
        if (boxJoint)
        {
            Destroy(boxJoint);
        }
    }
}
