using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
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
    public LeaderboardEntry(string name, int score, string hash = "")
    {
        this.name = name;
        this.score = score;
        this.hash = hash;
    }

    public string name;
    public int score;
    public string hash;
    public int rank = 0;
}

public class LeaderboardFirebaseUtils : MonoBehaviour
{
    const string FirebaseURI = "https://us-central1-ld53-2fbc3.cloudfunctions.net";

    static readonly HttpClient client = new HttpClient();

    private LeaderboardEntry[] leaderboard;
    private string playerName = "";
    private int score = -999;

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

    // -----------------------------------------------------------------
    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public string hashLeaderboardEntry(string playerName, int score)
    {
        string key = gameObject.GetComponent<ENV>().GetKey();
        string plainData = playerName + score + key;
        return ComputeSha256Hash(plainData);
    }
    // -----------------------------------------------------------------

    public async Task createLeaderboardEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;

        try
        {
            string hash = hashLeaderboardEntry(playerName, score);

            // Create new leaderboard entry
            LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score, hash);
            StringContent stringContent = new StringContent(JsonUtility.ToJson(newEntry), Encoding.UTF8, "application/json");

            // Send new leaderboard entry to Firebase database
            var response = await client.PostAsync(FirebaseURI + "/addScore", stringContent);
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

    public async Task getScores()
    {
        try
        {
            // Send new leaderboard entry to Firebase database
            var response = await client.PostAsync(FirebaseURI + "/getScores", null);
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