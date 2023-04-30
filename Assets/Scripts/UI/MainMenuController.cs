using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject buttonsContainer;
    public GameObject nameObject;

    public GameObject leaderboardScorePanel;
    public GameObject verticalGroup;

    public LeaderboardFirebaseUtils leaderboardUtils;

    public void ClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void OptionClick()
    {
        optionsMenu.SetActive(true);

        buttonsContainer.SetActive(false);
        nameObject.SetActive(false);
    }

    public async void ClickLeaderboard()
    {
        leaderboardUtils = gameObject.GetComponent<LeaderboardFirebaseUtils>();
        await leaderboardUtils.getScores();
        leaderboardScorePanel.SetActive(true);
        verticalGroup.SetActive(false);
    }

    public void ClickBackOption()
    {
        optionsMenu.SetActive(false);
        leaderboardScorePanel.SetActive(false);

        verticalGroup.SetActive(true);
        buttonsContainer.SetActive(true);
        nameObject.SetActive(true);
    }
}
