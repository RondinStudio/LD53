using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject buttonsContainer;
    public GameObject nameObject;

    private AudioSource buttonSound;

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

    public void ClickBackOption()
    {
        if (buttonSound)
        {
            if (!buttonSound.isPlaying)
                buttonSound.Play();
        }

        optionsMenu.SetActive(false);

        buttonsContainer.SetActive(true);
        nameObject.SetActive(true);
    }
}
