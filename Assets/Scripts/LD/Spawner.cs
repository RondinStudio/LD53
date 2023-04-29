using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Prefab;

    [SerializeField] List<Transform> SpawnPointsList;

    [SerializeField] int MaxObjects = 3;

    [SerializeField] float SpawnDelay = 0.5f;

    private List<GameObject> GameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        List<Transform> availableSpawners = SpawnPointsList;
        while (GameObjects.Count() < MaxObjects)
        {
            Transform spawn = availableSpawners[Random.Range(0, SpawnPointsList.Count())];
            GameObjects.Add(Instantiate(Prefab, spawn));
            availableSpawners.Remove(spawn);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
