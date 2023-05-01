using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject scorePanel;
    private AudioSource buttonSound;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Singleton") != null)
        {
            buttonSound = GameObject.FindGameObjectWithTag("Singleton").GetComponent<AudioSource>();
        }
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

    public void Resume()
    {
        Time.timeScale = 1f;
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }
    }
}
