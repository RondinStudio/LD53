using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer frontSpriteRenderer;

    public GoalValues goalValues;

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

    private void Start()
    {
        frontSpriteRenderer.color = GetColorFromEnum(goalValues.color);
    }
}
