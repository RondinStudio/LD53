using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    public float MinimalIncrementScore = 0f;

    public float MaximalIncrementScore = 0f;

    public float SpawnRateIncrement = 0f;

    public float SpawnRateFactor = 0f;

    public void UpdateSpawnRate(float timeScore)
    {
        if (timeScore >= MinimalIncrementScore && timeScore <= MaximalIncrementScore)
        {
            SpawnRateIncrement = timeScore * SpawnRateFactor;
        }
    }
}
