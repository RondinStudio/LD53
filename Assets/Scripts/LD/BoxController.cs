using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public BoxValues boxValues;
    public GameObject highlightObject;

    public GameObject particlesObject;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().mass = boxValues.weight;
    }

    public void EnableHighlightBoxes()
    {
        highlightObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableHighlightBoxes()
    {
        highlightObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
