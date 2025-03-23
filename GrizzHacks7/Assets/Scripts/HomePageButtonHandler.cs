using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class HomePageButtonHandler : MonoBehaviour
{

    public GameObject HomeUI;
    private GameObject HomeUIInstance;
    public GameObject ProfilePage;
    public GameObject SettingsPage;
    public float rotationSpeed = 100f;
    public float direction = -1; // -1 = left, 1 = right
    private bool leftButtonHeld = false;
    public Camera cam;
    public float ZoomChange;
    public float MinFOV, MaxFOV;

    void Start() {
    if (cam == null) {
        cam = Camera.main;
    }
}


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
                button.onClick.AddListener(Rotate);
            }
            else if (button.name == "RotateRightButton")
            {
                button.onClick.AddListener(Rotate);
            }
            else if (button.name == "ZoomInButton")
            {
                button.onClick.AddListener(Zoom);
            }
            else if (button.name == "ZoomOutButton")
            {
                button.onClick.AddListener(Zoom);
            }
        }
    }

    public void OnPointerDown(){
        leftButtonHeld = true;
    }

    public void OnPointerUp(){
        leftButtonHeld = false;
    }

    public void Update(){
        if (leftButtonHeld){
            Rotate();
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

    public void Rotate(){
        GameObject[] humanTransforms = GameObject.FindGameObjectsWithTag("HumanModel");
        foreach (GameObject humanTransform in humanTransforms) {
            humanTransform.transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);
        }
    }

    public void Zoom() {
        cam.fieldOfView += ZoomChange * direction;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, MinFOV, MaxFOV);
    }

}
