using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LD.Spawn
{
    public class ObstaclePicker
    {
        public static GameObject PickRandomObstacle(List<GameObject> candidates)
        {
            if (candidates != null && candidates.Count > 0)
            {
                float globalRarity = candidates.Sum(obstacle => obstacle.GetComponent<ObstacleProperties>().SpawnRateIncrement);

                float rarityRoll = globalRarity * Random.Range(0f, 1f);

                foreach (GameObject candidate in candidates)
                {
                    rarityRoll -= candidate.GetComponent<ObstacleProperties>().SpawnRateIncrement;
                    if (!(rarityRoll <= 0))
                        continue;

                    return candidate;
                }
            }
            return null;
        }
    }
}
