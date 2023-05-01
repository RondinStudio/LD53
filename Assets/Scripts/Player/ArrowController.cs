using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    private List<Properties> allConveyor;

    public void AddConveyor(Properties property)
    {
        allConveyor.Add(property);
    }

    private void Start()
    {
        foreach (GameObject conveyor in GameObject.FindGameObjectsWithTag("Conveyor"))
        {
            allConveyor.Add(conveyor.GetComponentInChildren<Properties>());
        }
    }

    private void Update()
    {
        foreach (Properties property in allConveyor)
        {
            if (property.HasBoxOn())
            {
                // Need an arrow if no arrow
            }
            else
            {
                // remove arrow is present
            }
        }
    }
}
