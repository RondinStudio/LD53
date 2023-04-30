using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class GameObjectCollectionChangedEventArgs : EventArgs
    {
        public GameObject NewItem;

        public Transform SpawnPointUsed;

        public GameObjectCollectionChangedEventArgs(GameObject newItems, Transform spawnPointsUsed)
        {
            NewItem = newItems;
            SpawnPointUsed = spawnPointsUsed;
        }
    }
}
