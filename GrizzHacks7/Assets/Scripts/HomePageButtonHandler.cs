using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePageButtonHandler : MonoBehaviour
{

    public GameObject HomeUI;
    private GameObject HomeUIInstance;
    public GameObject ProfilePage;
    public GameObject SettingsPage;

    void OnMouseDown()
    {    
        HomeUIInstance = Instantiate(HomeUI);

        Button[] buttons = HomeUIInstance.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == "ProfileButton") 
            {
                button.onClick.AddListener(OpenProfile);
            }
            else if (button.name == "DisplayIconsButton") 
            {
                button.onClick.AddListener(OpenIconsPage);
            }
            else if (button.name == "SettingsButton")
            {
                button.onClick.AddListener(OpenSettings);
            }
            else if (button.name == "RotateLeftButton")
            {
                button.onClick.AddListener(RotateLeft);
            }
            else if (button.name == "RotateRightButton")
            {
                button.onClick.AddListener(RotateRight);
            }
            else if (button.name == "ZoomInButton")
            {
                button.onClick.AddListener(ZoomIn);
            }
            else if (button.name == "ZoomOutButton")
            {
                button.onClick.AddListener(ZoomOut);
            }
        }
    }

    public void OpenProfile(){
        ProfilePage.SetActive(!ProfilePage.activeSelf);
    }

    public void OpenIconsPage(){
        SceneManager.LoadScene("StatsPages");
    }

    public void OpenSettings(){
        SettingsPage.SetActive(!SettingsPage.activeSelf);
    }
    
    public void RotateLeft(){

    }

    public void RotateRight(){

    }

    public void ZoomIn(){

    }

    public void ZoomOut(){

    }
}
