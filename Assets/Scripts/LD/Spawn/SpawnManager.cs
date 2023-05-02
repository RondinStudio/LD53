using Assets.Scripts.LD.Spawn;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Declarations
    [SerializeField] 
    private List<GameObject> Conveyors = new List<GameObject>();

    [SerializeField] 
    private List<GameObject> ObstacleSpawnPoints = new List<GameObject>();

    [SerializeField] 
    private List<GameObject> CratePrefab;

    [SerializeField] 
    private List<GameObject> ObstaclePrefab;

    [SerializeField] 
    private GameObject SpawnerPrefab;

    public int MaxCrateElements = 4;

    public int MaxObstacleElements = 4;

    public float CrateSpawnDelayOffset = 5.0f;

    [SerializeField]
    private float CrateSpawnMinimunDelay = 7.0f;

    [SerializeField]
    private float CrateSpawnDelayAcceleration = 1.0f;

    public float ObstacleSpawnDelayMinimum = 5.0f;

    public float ObstacleSpawnDelayMaximum = 15.0f;

    [SerializeField]
    private float TimeBeforeObstacleSpawnDelayIsMax = 80.0f;

    private Spawner _crateSpawner;

    private Spawner _obstacleSpawner;
    #endregion

    #region UI Management
    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            _crateSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();
            _obstacleSpawner = Instantiate(SpawnerPrefab).GetComponent<Spawner>();

            _crateSpawner.Prefab = CratePrefab;
            _crateSpawner.MaxElements = MaxCrateElements;
            _crateSpawner.SpawnDelay = CrateSpawnDelayOffset;

            foreach (List<GameObject> spawnpoints in Conveyors.Select(c => c.GetComponentInChildren<Spawner>().SpawnPointsList))
            {
                foreach (GameObject spawnpoint in spawnpoints)
                {
                    _crateSpawner.SpawnPointsList.Add(spawnpoint);
                }
            }
            _crateSpawner.OnGameObjectsListChange += CrateGameObjects_CollectionChanged;


            _obstacleSpawner.Prefab = ObstaclePrefab;
            _obstacleSpawner.SpawnPointsList = ObstacleSpawnPoints;
            _obstacleSpawner.MaxElements = MaxObstacleElements;
            _obstacleSpawner.SpawnDelay = ObstacleSpawnDelayMinimum;
            _obstacleSpawner.OnGameObjectsListChange += ObstacleGameObjects_CollectionChanged;

            StartCoroutine(_crateSpawner.Spawn());

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
                    ///the type of obstacle you want to spawn
                    GameObject randomObstacle = ObstaclePicker.PickRandomObstacle(spawner.Prefab);

                    ///the list of spawnpoints that can spawn randomObstacle
                    List<GameObject> availableSpawners = spawner.SpawnPointsList.
                        Where(s => s.GetComponent<ObstacleSpawnerProperties>().AvailablePrefabs.
                        Select(p => p.tag).
                        Contains(randomObstacle.tag)).ToList();
                    if(availableSpawners.Count > 0)
                    {
                        GameObject randomSpawnPoint = availableSpawners[UnityEngine.Random.Range(0, availableSpawners.Count - 1)];

                        ///the chosen prefab from spawnpoint
                        GameObject[] availablePrefabs = randomSpawnPoint.GetComponent<ObstacleSpawnerProperties>().AvailablePrefabs.Where(p => p.CompareTag(randomObstacle.tag)).ToArray();
                        GameObject randomPrefab = availablePrefabs[UnityEngine.Random.Range(0, availablePrefabs.Length)];

                        GameObject newObject = Instantiate(randomPrefab, randomSpawnPoint.transform);
                        spawner.GameObjects.Add(newObject);
                        spawner.OnGameObjectsListChange?.Invoke(this, new GameObjectCollectionChangedEventArgs(newObject, randomSpawnPoint));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            yield return new WaitForSeconds(spawner.SpawnDelay);
            timeScore = Time.timeSinceLevelLoad;
            foreach (GameObject obstacle in spawner.Prefab)
            {
                obstacle.GetComponent<ObstacleProperties>().UpdateSpawnRate(timeScore);
            }
        }
    }
    #endregion

    #region EventHandlers
    private void CrateGameObjects_CollectionChanged(object sender, EventArgs e)
    {
        if (_crateSpawner.SpawnDelay >= CrateSpawnMinimunDelay)
        {
            _crateSpawner.SpawnDelay = CrateSpawnDelayOffset + (-CrateSpawnDelayAcceleration * Time.timeSinceLevelLoad);
        }
    }

    private void ObstacleGameObjects_CollectionChanged(object sender, EventArgs e)
    {
        GameObject spawnPointUsed = (e as GameObjectCollectionChangedEventArgs).SpawnPointUsed;

        try
        {
            _obstacleSpawner.SpawnPointsList.Remove(spawnPointUsed);
            _obstacleSpawner.SpawnDelay = ObstacleSpawnDelayMinimum + (Time.timeSinceLevelLoad * (ObstacleSpawnDelayMaximum - ObstacleSpawnDelayMinimum) / TimeBeforeObstacleSpawnDelayIsMax);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    #endregion
}
