using System;
using System.Net.Http;
using System.Text;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public LeaderboardEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string name;
    public int score;
    public int rank = 0;

    public override string ToString()
    {
        return rank + " - " + name + " (" + score + ")";
    }
}

public class leaderboard : MonoBehaviour
{
    const string FirebaseURI = "https://us-central1-ld53-2fbc3.cloudfunctions.net/addScore";

    static readonly HttpClient client = new HttpClient();

    public async Task<LeaderboardEntry[]> createLeaderboardEntry(string playerName, int score)
    {
        try
        {
            // Create new leaderboard entry
            LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score);
            StringContent stringContent = new StringContent(JsonUtility.ToJson(newEntry), Encoding.UTF8, "application/json");

            // Send new leaderboard entry to Firebase database
            var response = await client.PostAsync(FirebaseURI, stringContent);
            if (response != null)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                LeaderboardEntry[] entries = JsonHelper.FromJson<LeaderboardEntry>(jsonString);

                return entries;

                // int lastRank = 0;
                // foreach (LeaderboardEntry entry in entries)
                // {
                //     if (entry.rank != lastRank + 1)
                //     {
                //         Debug.Log("...");
                //     }
                //     Debug.Log(entry);
                //     lastRank = entry.rank;
                // }
            }
        }
        catch (HttpRequestException e)
        {
            Debug.Log(e.Message);
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
