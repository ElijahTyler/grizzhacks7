using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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
    public InputField descriptionField;
    private GameObject currentNote = null;
    private int userID;

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
                button.onClick.AddListener(DeleteNoteButton);
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
                button.onClick.AddListener(DeleteNoteButton);
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
                // Debug.Log("Dot created");
            } else {
                // Debug.Log("Dot not created");
            }
        }
    }

    void PlaceDot(Vector3 position, Vector3 normal, Transform parent)
    {
        GameObject note = Instantiate(notePrefab, position, Quaternion.LookRotation(normal));
        note.transform.SetParent(parent, true);

        viewNotePrefab.SetActive(true);
        isNoteUIActive = true;

        StartCoroutine(CreateNote(note));

        currentNote = note;
    }

    IEnumerator CreateNote(GameObject note) {
        WWWForm form = new WWWForm();
        string coord_x = note.transform.position.x.ToString();
        string coord_y = note.transform.position.y.ToString();
        string coord_z = note.transform.position.z.ToString();
        form.AddField("coord_x", coord_x);
        form.AddField("coord_y", coord_y);
        form.AddField("coord_z", coord_z);
        form.AddField("user_id", GameObject.FindGameObjectWithTag("UserID").GetComponent<UserIDContainer>().id.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SQLconnect/create_note.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0") {
            WWWForm form2 = new WWWForm();
            UnityWebRequest www2 = UnityWebRequest.Post("http://localhost/SQLconnect/get_newest_note.php", form2);
            yield return www2.SendWebRequest();

            if (www2.downloadHandler.text != "0") {
                string newNoteID = www2.downloadHandler.text.Substring(1);
                NoteIDContainer noteIDContainer = note.GetComponent<NoteIDContainer>();
                if (noteIDContainer != null) {
                    noteIDContainer.id = Int32.Parse(newNoteID);
                    Debug.Log("Note ID successfully set!");
                } else {
                    Debug.LogWarning("NoteIDContainer component not found on note!");
                }
            } else if (www.downloadHandler.text == "fart") {
                Debug.Log("Fortnite");
            }
            else {
                Debug.LogError("Error fetching newest note ID: " + www2.error);
            }
        } else {
            Debug.LogWarning(www.downloadHandler.text);
        }
    }

    IEnumerator AddDescription() {
        WWWForm form = new WWWForm();
        form.AddField("note_id", currentNote.GetComponent<NoteIDContainer>().id.ToString());
        form.AddField("description", descriptionField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SQLconnect/add_description.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0") {
            Debug.Log("Successfully updated description");
        }
    }

    IEnumerator DeleteNote() {
        WWWForm form = new WWWForm();
        form.AddField("note_id", currentNote.GetComponent<NoteIDContainer>().id.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/SQLconnect/delete_note.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0") {
            Debug.Log("Successfully deleted note");
        }
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
        currentNote = null;
    }

    void DeleteNoteButton(){
        StartCoroutine(DeleteNote());
        viewNotePrefab.SetActive(false);
        editNotePrefab.SetActive(false);
        isNoteUIActive = false;
        Destroy(currentNote);
        currentNote = null;
    }

    void SaveDetails(){
        StartCoroutine(AddDescription());
        viewNotePrefab.SetActive(true);
        editNotePrefab.SetActive(false);
        currentNote = null;
    }

    void EditDetails(){
        editNotePrefab.SetActive(true);
        viewNotePrefab.SetActive(false);
    }


}
