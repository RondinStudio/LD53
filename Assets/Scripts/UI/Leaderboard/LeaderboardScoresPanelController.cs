using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScoresPanelController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] texts;

    private string[] playerNames;
    private int[] playerScores;


    // Start is called before the first frame update
    void Start()
    {
        // playerNames, playerScores = GetScores() jsp trop, faudra attendre aussi genre en lan�ant un timer ou en attendant une r�ponse
    }

    // Update is called once per frame
    void Update()
    {
        // Ici si ton timer est fini ou si t'as re�u les scores

        for (int i = 0; i < playerNames.Length; i++)
        {
            texts[i].text = i + playerNames[i] + playerScores[i];
        }
    }
}
