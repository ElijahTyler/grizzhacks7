using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userNameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SQLconnect/login.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text.Length > 0)
        {
            DBManager.username = userNameField.text;
            GameObject userIDContainer = new GameObject();
            UserIDContainer cont = userIDContainer.AddComponent<UserIDContainer>();
            userIDContainer.tag = "UserID";
            string poop = www.downloadHandler.text;
            // poop.Substring(1);
            Debug.Log(poop);
            cont.id = Int32.Parse(poop);
            Debug.Log(cont.id);

            DontDestroyOnLoad(userIDContainer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomePage");
        }
        else
        {
            Debug.Log("User login failed. Error#: " + www.downloadHandler.text);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (userNameField.text.Length >= 4
                && passwordField.text.Length >= 8);
    }
}
