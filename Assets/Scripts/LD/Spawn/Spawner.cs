using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefab;

    public List<Transform> SpawnPointsList = new ();

    public int MaxElements = 0;

    public float SpawnDelay = 0.5f;

    public readonly List<GameObject> GameObjects = new();

    public EventHandler OnGameObjectsListChange;

    public IEnumerator Spawn()
    {
        while (GameObjects.Count() < MaxElements || MaxElements == 0)
        {
            if(SpawnPointsList.Count > 0 && SpawnPointsList != null && Prefab.Count > 0 && Prefab != null)
            {
                try
                {
                    GameObject randomPrefab = Prefab[UnityEngine.Random.Range(0, Prefab.Count() - 1)];
                    Transform randomSpawnPoint = SpawnPointsList[UnityEngine.Random.Range(0, SpawnPointsList.Count() - 1)];

                    GameObject newObject = Instantiate(randomPrefab, randomSpawnPoint);
                    GameObjects.Add(newObject);
                    OnGameObjectsListChange?.Invoke(this, new GameObjectCollectionChangedEventArgs(newObject, randomSpawnPoint));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}