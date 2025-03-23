using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class createAccount : MonoBehaviour
{
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public TMP_InputField emailField;

    public Button submitButton;

    public TMP_Dropdown sexSelect;
    private char selectedChar;

    public void OnDropdownValueChanged()
    {
        string selectedText = sexSelect.options[sexSelect.value].text;

        if (!string.IsNullOrEmpty(selectedText) && selectedText.Length == 1)
        {
            selectedChar = selectedText[0]; // convert to char
            Debug.Log("Selected Char: " + selectedChar);
        }
        else
        {
            Debug.Log("Selected Option isnt a single character");
        }
    }

    public void CallCreateAccount()
    {
        StartCoroutine(CreateAccount());
    }

    IEnumerator CreateAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userNameField.text);
        form.AddField("password", passwordField.text);
        form.AddField("email", emailField.text);
        form.AddField("sex", sexSelect.options[sexSelect.value].text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SQLconnect/create_account.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("User created successfully");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed lol idiot. Error #: " + www.downloadHandler.text);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (userNameField.text.Length >= 4 
                && passwordField.text.Length >= 8 && emailField.text.Length >= 5
                && sexSelect.options[sexSelect.value].text.Length == 1);
    }
}
