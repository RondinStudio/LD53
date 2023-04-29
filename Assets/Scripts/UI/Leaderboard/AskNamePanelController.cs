using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AskNamePanelController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameField;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private ScoreController scoreController;

    private void Start()
    {
        scoreController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoreController>();
    }

    public void OnSendNamePressed()
    {
        // Envoyer fonction qui envoie les stats avec addScore(nameField.text, scoreController.GetScore())
    }

    public void SendScoreToText()
    {
        scoreText.text = scoreText.text + scoreController.GetScore();
    }
}
