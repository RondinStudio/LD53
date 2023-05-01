using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class GameObjectCollectionChangedEventArgs : EventArgs
    {
        public GameObject NewItem;

        public GameObject SpawnPointUsed;

        public GameObjectCollectionChangedEventArgs(GameObject newItems, GameObject spawnPointsUsed)
        {
            NewItem = newItems;
            SpawnPointUsed = spawnPointsUsed;
        }
    }
}
