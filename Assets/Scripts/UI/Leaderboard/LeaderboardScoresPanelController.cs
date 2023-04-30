using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScoresPanelController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] scoreTexts;
    [SerializeField]
    private TextMeshProUGUI[] nameTexts;
    [SerializeField]
    private TextMeshProUGUI[] rankTexts;

    [SerializeField]
    private Color32 highlightColor;

    private LeaderboardFirebaseUtils leaderboardUtils;

    void Start()
    {
        // Here, we assume that leaderboard is correctly fetched (createLeaderboardEntry finished with success)
        leaderboardUtils = gameObject.GetComponentInParent<LeaderboardFirebaseUtils>();

        int previousRank = 0;
        int offset = 0;
        for (int i = 0; i < leaderboardUtils.GetLeaderboard().Length; i++)
        {
            LeaderboardEntry entry = leaderboardUtils.GetLeaderboard()[i];

            // Display "..." between the top scores and the scores around the player. Add an offset of 1 to array index.
            if (entry.rank != previousRank + 1)
            {
                nameTexts[i].text = "...";
                offset = 1;
            }

            // Fill leaderboard text
            nameTexts[i + offset].text = "#" + entry.rank + " - " + entry.name + " - " + entry.score + "pts";

            // Add style to current player score
            if (entry.name == leaderboardUtils.getPlayerName() && entry.score == leaderboardUtils.getScore()) {
                nameTexts[i + offset].color = highlightColor;
                nameTexts[i + offset].fontStyle = FontStyles.Bold;
            }

            previousRank = entry.rank;
        }
    }
}
