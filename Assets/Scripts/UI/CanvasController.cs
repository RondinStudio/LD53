using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject scorePanel;

    public bool canPause = true;

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.active)
            {
                pauseMenu.SetActive(false);
                scorePanel.SetActive(true);
                Time.timeScale = 1f;
            } else if (canPause)
            {
                pauseMenu.SetActive(true);
                scorePanel.SetActive(false);
                Time.timeScale = 0f;
            }

        }
    }
}
