using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    [SerializeField]
    private GameObject healthBoxes;

    public GameObject EnterNamePanel;
    public GameObject ScorePanel;
    public CanvasController canvasController;

    public void RemoveHealth()
    {
        health--;
        Destroy(healthBoxes.transform.GetChild(0).gameObject);
        CheckForLoseCondition();
    }

    private void CheckForLoseCondition()
    {
        if (health <= 0)
        {
            EnterNamePanel.SetActive(true);
            ScorePanel.SetActive(false);

            canvasController.canPause = false;

            // We will need to cut the sound of the drone here at least
        }
    }
}
