using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;

public class MainMenu : MonoBehaviour
{
    public TMP_Text userDisplay;
    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            userDisplay.text = "User: " + DBManager.username;
        }
    }

    public void GoToCreateAccount()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLogin()
    {
        if (DBManager.LoggedIn) {
            SceneManager.LoadScene("HomePage");
            return;
        }

        SceneManager.LoadScene(2);
    }
    public void Logout()
    {
        DBManager.LogOut();
        userDisplay.text = "Login or create an account to continue";
    }
}
