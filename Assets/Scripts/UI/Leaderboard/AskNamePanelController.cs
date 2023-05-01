using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AskNamePanelController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameField;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public GameObject leaderboardScorePanel;

    private ScoreController scoreController;

    private LeaderboardFirebaseUtils leaderboardUtils;

    private AudioSource buttonSound;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Singleton") != null && !buttonSound)
        {
            buttonSound = GameObject.FindGameObjectWithTag("Singleton").GetComponent<AudioSource>();
        }

        scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
        leaderboardUtils = gameObject.GetComponentInParent<LeaderboardFirebaseUtils>();

        SendScoreToText(); // add this here?
    }

    public async void OnSendNamePressed()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        await leaderboardUtils.createLeaderboardEntry(nameField.text, scoreController.GetScore());
        leaderboardScorePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SendScoreToText()
    {
        scoreText.text = scoreText.text + scoreController.GetScore();
    }
}
