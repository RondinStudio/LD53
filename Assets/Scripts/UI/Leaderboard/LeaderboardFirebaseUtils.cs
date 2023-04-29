using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
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
}

public class LeaderboardFirebaseUtils : MonoBehaviour
{
    const string FirebaseURI = "https://us-central1-ld53-2fbc3.cloudfunctions.net/addScore";

    static readonly HttpClient client = new HttpClient();

    private LeaderboardEntry[] leaderboard;
    private string playerName;
    private int score;

    public LeaderboardEntry[] GetLeaderboard()
    {
        return leaderboard;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getScore()
    {
        return score;
    }

    public async Task createLeaderboardEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;

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
                // Save leaderboard
                leaderboard = JsonHelper.FromJson<LeaderboardEntry>(jsonString);
            }
        }
        catch (HttpRequestException e)
        {
            Debug.Log(e.Message);
            // Set leaderboard to an empty array, to avoid crashing the leaderboard panel
            leaderboard = new LeaderboardEntry[0];
        }
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