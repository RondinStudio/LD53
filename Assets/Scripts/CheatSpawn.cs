using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Spawner> spawners;

    void Start()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.StartCoroutine(spawner.Spawn());
        }
    }
}
