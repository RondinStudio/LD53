using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New box value", menuName = "Box values", order = 1)]
public class BoxValues : ScriptableObject
{
    public EColor color;
    public int scoreValue;
}
