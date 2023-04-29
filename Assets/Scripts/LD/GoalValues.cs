using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Red,
    Green,
    Blue
}

[CreateAssetMenu(fileName = "New goal value", menuName = "Goal values", order = 1)]
public class GoalValues : ScriptableObject
{
    public EColor color;
}
