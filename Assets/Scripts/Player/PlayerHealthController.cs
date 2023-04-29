using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    [SerializeField]
    private TextMeshProUGUI healthText;

    public GameObject EnterNamePanel;
    public GameObject ScorePanel;

    public void RemoveHealth()
    {
        health--;
        healthText.text = "Health : " + health;
        CheckForLoseCondition();
    }

    private void CheckForLoseCondition()
    {
        if (health <= 0)
        {
            EnterNamePanel.SetActive(true);
            ScorePanel.SetActive(false);
            Destroy(gameObject);
        }
    }
}
