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

    private AudioSource buttonSound;

    public GameObject leaderboardScorePanel;
    public GameObject verticalGroup;

    public LeaderboardFirebaseUtils leaderboardUtils;
	
	private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Singleton") != null)
        {
            buttonSound = GameObject.FindGameObjectWithTag("Singleton").GetComponent<AudioSource>();
        }
    }

    public void ClickStart()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickExit()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }
        Application.Quit();
    }

    public void OptionClick()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        optionsMenu.SetActive(true);

        buttonsContainer.SetActive(false);
        nameObject.SetActive(false);
    }

    public async void ClickLeaderboard()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        leaderboardUtils = gameObject.GetComponent<LeaderboardFirebaseUtils>();
        await leaderboardUtils.getScores();
        leaderboardScorePanel.SetActive(true);
        verticalGroup.SetActive(false);
    }

    public void ClickBackOption()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        optionsMenu.SetActive(false);
        leaderboardScorePanel.SetActive(false);

        verticalGroup.SetActive(true);
        buttonsContainer.SetActive(true);
        nameObject.SetActive(true);
    }
}
