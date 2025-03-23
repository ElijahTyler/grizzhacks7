using System;
using UnityEngine;
using UnityEngine.UI;

public class NoteHandler : MonoBehaviour
{
    public GameObject notePrefab; 
    public GameObject viewNotePrefab; 
    public GameObject editNotePrefab;
    public bool isNoteUIActive = false;
    public Text NoteName;
    public InputField NoteNameField;
    public Button saveButton;
    public Button editButton;
    public Button viewCloseButton;
    public Button editCloseButton;
    public Button deleteButton;
    public String humanModelTag;

    void Start()
    {
        if (viewNotePrefab != null || editNotePrefab != null)
        {
            viewNotePrefab.SetActive(false);
            editNotePrefab.SetActive(false);
        }

        Button[] buttons = viewNotePrefab.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == "CloseButton") 
            {
                button.onClick.AddListener(CloseNoteUI);
            }
            else if (button.name == "EditButton")
            {
                button.onClick.AddListener(EditDetails);
            }
            else if (button.name == "DeleteButton") 
            {
                button.onClick.AddListener(DeleteNote);
            }
        }

        Button[] editButtons = editNotePrefab.GetComponentsInChildren<Button>();
        foreach (Button button in editButtons)
        {
            if (button.name == "CloseButton") 
            {
                button.onClick.AddListener(CloseNoteUI);
            }
            else if (button.name == "DeleteButton") 
            {
                button.onClick.AddListener(DeleteNote);
            }
            else if (button.name == "SaveButton")
            {
                button.onClick.AddListener(SaveDetails);
            }
        }

    }

    void Update()
    {
        if (!isNoteUIActive && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            GameObject modelTransform = GameObject.FindGameObjectWithTag(humanModelTag);

            if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.tag == humanModelTag))
            {
                PlaceDot(hit.point, hit.normal, hit.transform);
                Debug.Log("Dot created");
            } else {
                Debug.Log("Dot not created");
            }
        }
    }

    void PlaceDot(Vector3 position, Vector3 normal, Transform parent)
    {
        GameObject note = Instantiate(notePrefab, position, Quaternion.LookRotation(normal));
        note.transform.SetParent(parent, true); 
        viewNotePrefab.SetActive(true);
        isNoteUIActive = true;
    }

    void OnMouseDown()
    {
        if (isNoteUIActive) return; 

        viewNotePrefab.SetActive(true); 
        isNoteUIActive = true;
    }

    void CloseNoteUI()
    {
        viewNotePrefab.SetActive(false);
        editNotePrefab.SetActive(false);
        isNoteUIActive = false;
    }

    void DeleteNote(){
        viewNotePrefab.SetActive(false);
        editNotePrefab.SetActive(false);
        isNoteUIActive = false;
        // ! Destroy(gameObject);
    }

    void SaveDetails(){
        viewNotePrefab.SetActive(true);
        editNotePrefab.SetActive(false);
    }

    void EditDetails(){
        editNotePrefab.SetActive(true);
        viewNotePrefab.SetActive(false);
    }


}
