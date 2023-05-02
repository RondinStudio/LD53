using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScoresPanelController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] scoreTexts;
    [SerializeField]
    private TextMeshProUGUI[] nameTexts;
    [SerializeField]
    private TextMeshProUGUI[] rankTexts;

    private LeaderboardFirebaseUtils leaderboardUtils;
    private AudioSource buttonSound;


    public void GoToMainMenu()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();

        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Singleton") != null && !buttonSound)
        {
            buttonSound = GameObject.FindGameObjectWithTag("Singleton").GetComponent<AudioSource>();
        }
    }

    public void Refresh()
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
                rankTexts[i].text = "";
                nameTexts[i].text = "...";
                scoreTexts[i].text = "";
                offset = 1;
            }

            // Fill leaderboard text
            rankTexts[i + offset].text = "#" + entry.rank;
            nameTexts[i + offset].text = entry.name;
            scoreTexts[i + offset].text = entry.score + "pts";

            // Add style to current player score
            if (entry.name == leaderboardUtils.getPlayerName() && entry.score == leaderboardUtils.getScore())
            {
                foreach (var textItem in new TextMeshProUGUI[][] { rankTexts, nameTexts, scoreTexts })
                {
                    textItem[i + offset].outlineColor = new Color32(0, 0, 0, 255); ;
                    textItem[i + offset].outlineWidth = 0.3f;
                    if (entry.rank > 3)
                    {
                        textItem[i + offset].color = new Color32(255, 255, 255, 255);
                    }
                }
            }

            previousRank = entry.rank;
        }
    }
}
