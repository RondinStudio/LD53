using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public BoxValues boxValues;

    private Color GetColorFromEnum(EColor color)
    {
        switch (color)
        {
            case EColor.Red:
                return Color.red;
            case EColor.Green:
                return Color.green;
            case EColor.Blue:
                return Color.blue;
            default:
                return Color.white;
        }
    }

    private int GetMultiplierFromSize(ESize size)
    {
        switch (size)
        {
            case ESize.Small:
                return 1;
            case ESize.Medium:
                return 2;
            case ESize.Large:
                return 3;
            default:
                return 1;
        }
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GetColorFromEnum(boxValues.color);
        gameObject.GetComponent<Rigidbody2D>().mass *= GetMultiplierFromSize(boxValues.size);
        gameObject.GetComponent<Transform>().localScale *= GetMultiplierFromSize(boxValues.size);
    }
}
