using Assets.Scripts.LD.Spawn;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Declarations
    [SerializeField] 
    private List<Transform> ConveyorSpawnPoints = new List<Transform>();

    [SerializeField] 
    private List<Transform> ObstacleSpawnPoints = new List<Transform>();

    [SerializeField] 
    private List<GameObject> ConveyorPrefab;

    [SerializeField] 
    private List<GameObject> CratePrefab;

    [SerializeField] 
    private List<GameObject> ObstaclePrefab;

    [SerializeField] 
    private GameObject SpawnerPrefab;

    [SerializeField] 
    private int MaxConveyorElements = 3;

    public int MaxCrateElements = 4;

    public int MaxObstacleElements = 4;

    public float ConveyorSpawnDelay = 5.0f;

    public float CrateSpawnDelay = 1.0f;

    public float ObstacleSpawnDelay = 1.0f;

    private Spawner _conveyorSpawner;

    private Spawner _crateSpawner;

    private Spawner _obstacleSpawner;
    #endregion

    #region UI Management
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _conveyorSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();
            _crateSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();
            _obstacleSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();


            _conveyorSpawner.Prefab = ConveyorPrefab;
            _conveyorSpawner.SpawnPointsList = ConveyorSpawnPoints;
            _conveyorSpawner.MaxElements = MaxConveyorElements;
            _conveyorSpawner.SpawnDelay = ConveyorSpawnDelay;
            _conveyorSpawner.OnGameObjectsListChange += ConveyorGameObjects_CollectionChanged;

            _crateSpawner.Prefab = CratePrefab;
            _crateSpawner.MaxElements = MaxCrateElements;
            _crateSpawner.SpawnDelay = CrateSpawnDelay;

            _obstacleSpawner.Prefab = ObstaclePrefab;
            _obstacleSpawner.SpawnPointsList = ObstacleSpawnPoints;
            _obstacleSpawner.MaxElements = MaxObstacleElements;
            _obstacleSpawner.SpawnDelay = ObstacleSpawnDelay;
            _obstacleSpawner.OnGameObjectsListChange += ObstacleGameObjects_CollectionChanged;

            StartCoroutine(_crateSpawner.Spawn());

            StartCoroutine(_conveyorSpawner.Spawn());

            StartCoroutine(ObstacleSpawn(_obstacleSpawner));
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    #endregion

    #region Methods
    public IEnumerator ObstacleSpawn(Spawner spawner)
    {
        float timeScore = Time.timeSinceLevelLoad;
        while (spawner.GameObjects.Count < spawner.MaxElements || spawner.MaxElements == 0)
        {
            if (spawner.SpawnPointsList.Count > 0 && spawner.SpawnPointsList != null && spawner.Prefab.Count > 0 && spawner.Prefab != null)
            {
                try
                {
                    GameObject randomPrefab = ObstaclePicker.PickRandomObstacle(spawner.Prefab);
                    Transform randomSpawnPoint = spawner.SpawnPointsList[UnityEngine.Random.Range(0, spawner.SpawnPointsList.Count - 1)];

                    GameObject newObject = Instantiate(randomPrefab, randomSpawnPoint);
                    spawner.GameObjects.Add(newObject);
                    spawner.OnGameObjectsListChange?.Invoke(this, new GameObjectCollectionChangedEventArgs(newObject, randomSpawnPoint));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            yield return new WaitForSeconds(spawner.SpawnDelay);
            timeScore = Time.timeSinceLevelLoad ;
            foreach (GameObject obstacle in spawner.Prefab)
            {
                obstacle.GetComponent<ObstacleProperties>().UpdateSpawnRate(timeScore);
            }
        }
    }
    #endregion

    #region EventHandlers
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

    private void ObstacleGameObjects_CollectionChanged(object sender, EventArgs e)
    {
        Transform spawnPointUsed = (e as GameObjectCollectionChangedEventArgs).SpawnPointUsed;

        try
        {
            _obstacleSpawner.SpawnPointsList.Remove(spawnPointUsed);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    #endregion
}
