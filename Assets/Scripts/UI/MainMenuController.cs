using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject buttonsContainer;
    public GameObject nameObject;

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

    public void ClickBackOption()
    {
        optionsMenu.SetActive(false);

        buttonsContainer.SetActive(true);
        nameObject.SetActive(true);
    }
}
