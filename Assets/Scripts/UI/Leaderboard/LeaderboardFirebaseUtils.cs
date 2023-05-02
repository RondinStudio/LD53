using System;
using System.Collections;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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

    public IEnumerator createLeaderboardEntry(string playerName, int score, EventHandler finishedEvent)
    {
        this.playerName = playerName;
        this.score = score;

        string hash = hashLeaderboardEntry(playerName, score);

        // Create new leaderboard entry
        LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score, hash);
        WWWForm form = new WWWForm();
        form.AddField("name", playerName);
        form.AddField("score", score);
        form.AddField("hash", hash);

        // Send new leaderboard entry to Firebase database
        using (UnityWebRequest www = UnityWebRequest.Post(FirebaseURI + "/addScore", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                // Save leaderboard
                leaderboard = JsonHelper.FromJson<LeaderboardEntry>(jsonString);
                LeaderboardScoresPanelController leaderboardController = gameObject.GetComponentInChildren<LeaderboardScoresPanelController>();
                leaderboardController.Refresh();
            }
            finishedEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public IEnumerator getScores()
    {
        // Send new leaderboard entry to Firebase database
        using (UnityWebRequest www = UnityWebRequest.Post(FirebaseURI + "/getScores", ""))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                // Set leaderboard to an empty array, to avoid crashing the leaderboard panel
                leaderboard = new LeaderboardEntry[0];
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                // Save leaderboard
                leaderboard = JsonHelper.FromJson<LeaderboardEntry>(jsonString);
                LeaderboardScoresPanelController leaderboardController = gameObject.GetComponentInChildren<LeaderboardScoresPanelController>();
                leaderboardController.Refresh();
            }
        }
    }
}