using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    public float MinimalIncrementScore = 0f;

    public float MaximalIncrementScore = 0f;

    public float SpawnRateIncrement = 0f;

    public float SpawnRateFactor = 0f;

    private void Start()
    {
        MinimalIncrementScore = 0f;
        MaximalIncrementScore = 0f;
        SpawnRateIncrement = 0f;
        SpawnRateFactor = 0f;
    }

    public void UpdateSpawnRate(float timeScore)
    {
        if (timeScore >= MinimalIncrementScore && timeScore <= MaximalIncrementScore)
        {
            SpawnRateIncrement = (timeScore - MinimalIncrementScore) * SpawnRateFactor;
        }
    }
}
