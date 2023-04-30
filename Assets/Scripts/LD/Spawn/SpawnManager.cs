using System;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> ConveyorSpawnPoints = new List<Transform>();

    [SerializeField] List<GameObject> ConveyorPrefab;

    [SerializeField] List<GameObject> CratePrefab;

    [SerializeField] List<GameObject> ObstaclePrefab;

    [SerializeField] int MaxConveyorElements = 3;

    [SerializeField] GameObject SpawnerPrefab;

    public int MaxCrateElements = 4;

    public float ConveyorSpawnDelay = 5.0f;

    public float CrateSpawnDelay = 1.0f;

    public float ObstacleSpawnDelay = 1.0f;

    private Spawner _conveyorSpawner;

    private Spawner _crateSpawner;

    private GameObject _obstacleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _conveyorSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();
            _crateSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();

            _conveyorSpawner.Prefab = ConveyorPrefab;
            _conveyorSpawner.SpawnPointsList = ConveyorSpawnPoints;
            _conveyorSpawner.MaxElements = MaxConveyorElements;
            _conveyorSpawner.SpawnDelay = ConveyorSpawnDelay;
            _conveyorSpawner.OnGameObjectsListChange += ConveyorGameObjects_CollectionChanged;

            _crateSpawner.Prefab = CratePrefab;
            _crateSpawner.MaxElements = MaxCrateElements;
            _crateSpawner.SpawnDelay = CrateSpawnDelay;

            _crateSpawner.StartCoroutine(_crateSpawner.Spawn());

            _conveyorSpawner.StartCoroutine(_conveyorSpawner.Spawn());
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void ConveyorGameObjects_CollectionChanged(object sender, EventArgs e)
    {
        GameObject newItem = (e as GameObjectCollectionChangedEventArgs).NewItem;
        Transform spawnPointUsed = (e as GameObjectCollectionChangedEventArgs).SpawnPointUsed;

        try
        {
            _crateSpawner.SpawnPointsList.AddRange(newItem.GetComponentInChildren<Spawner>().SpawnPointsList);
            _conveyorSpawner.SpawnPointsList.Remove(spawnPointUsed);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
