using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollisionDetector : MonoBehaviour
{
    private FixedJoint2D boxJoint;

    private BoxController hoveringBoxController;

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

                    if (hoveringBoxController)
                    {
                        hoveringBoxController.DisableHighlightBoxes();
                        hoveringBoxController = null;
                    }
                }
            }
            if (!hoveringBoxController && !boxJoint)
            {
                hoveringBoxController = collision.gameObject.GetComponent<BoxController>();
                hoveringBoxController.EnableHighlightBoxes();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hoveringBoxController)
        {
            if (hoveringBoxController == collision.gameObject.GetComponent<BoxController>())
            {
                hoveringBoxController.DisableHighlightBoxes();
                hoveringBoxController = null;
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

    public GameObject GetConnectedObject()
    {
        if (boxJoint)
        {
            return boxJoint.connectedBody.gameObject;
        }
        return null;
    }
}
