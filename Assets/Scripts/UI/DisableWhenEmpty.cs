using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableWhenEmpty : MonoBehaviour
{
    public Button sendNameButton;

    private TMP_InputField inputField;

    private void Start()
    {
        inputField = gameObject.GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (inputField.text != "")
        {
            sendNameButton.interactable = true;
        }
        else
        {
            sendNameButton.interactable = false;
        }
    }
}
