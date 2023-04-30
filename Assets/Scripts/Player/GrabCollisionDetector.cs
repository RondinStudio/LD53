using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabCollisionDetector : MonoBehaviour
{
    private FixedJoint2D boxJoint;

    private bool _isCarrying = false;

    private BoxController hoveringBoxController;

    [SerializeField]
    private Rigidbody2D grabRb;

    [SerializeField]
    private AudioSource grabSound;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            if (!boxJoint)
            {
                if (_isCarrying)
                {
                    if (!grabSound.isPlaying)
                        grabSound.Play();

                    // creates joint
                    boxJoint = grabRb.gameObject.AddComponent<FixedJoint2D>();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!grabSound.isPlaying)
                    grabSound.Play();
                Destroy(boxJoint);
                _isCarrying = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isCarrying = true;
            }
        }
        gameObject.GetComponent<Rigidbody2D>().position = grabRb.position;
    }

    public void DestroyJointIfPresent()
    {
        if (boxJoint)
        {
            Destroy(boxJoint);
            if (!grabSound.isPlaying)
                grabSound.Play();
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
