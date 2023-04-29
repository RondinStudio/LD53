using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESize
{
    Small,
    Medium,
    Large
}

[CreateAssetMenu(fileName = "New box value", menuName = "Box values", order = 1)]
public class BoxValues : ScriptableObject
{
    public EColor color;
    public ESize size;
    public int scoreValue;
}
