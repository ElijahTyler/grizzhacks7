using UnityEngine;
using UnityEngine.UI;

public class NoteHandler : MonoBehaviour
{
    public GameObject notePrefab; 
    public GameObject noteUIPrefab; 
    public bool isNoteUIActive = false;
    public Text NoteName;
    public InputField NoteNameField;
    public Button saveButton;
    public Button editButton;
    public Button closeButton;
    public Button deleteButton;

    void Start()
    {
        if (noteUIPrefab != null)
        {
            noteUIPrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (!isNoteUIActive && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                PlaceDot(hit.point, hit.normal, hit.transform);
            }
        }
    }

    void PlaceDot(Vector3 position, Vector3 normal, Transform parent)
    {
        GameObject note = Instantiate(notePrefab, position, Quaternion.LookRotation(normal));
        note.transform.SetParent(parent, true); 
        NoteInteraction noteInteraction = note.AddComponent<NoteInteraction>();
        noteInteraction.noteUIPrefab = noteUIPrefab;
        noteInteraction.noteHandler = this;
    }
    
    public void SetNoteUIActive(bool isActive)
    {
        isNoteUIActive = isActive;
    }
}

public class NoteInteraction : MonoBehaviour
{
    public GameObject noteUIPrefab;
    private GameObject noteUIInstance;
    public NoteHandler noteHandler;

    void OnMouseDown()
    {
        if (noteHandler.isNoteUIActive) return; 

        noteUIInstance = Instantiate(noteUIPrefab);
        noteUIInstance.SetActive(true); 
        
        Button[] buttons = noteUIInstance.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == "CloseButton") 
            {
                button.onClick.AddListener(CloseNoteUI);
            }
            else if (button.name == "DeleteButton") 
            {
                button.onClick.AddListener(DeleteDot);
            }
            else if (button.name == "SaveButton")
            {
                button.onClick.AddListener(SaveName);
            }
            else if (button.name == "EditButton")
            {
                button.onClick.AddListener(EditDetails);
            }
        }
        
        noteHandler.SetNoteUIActive(true);
    }

     void CloseNoteUI()
    {
        Destroy(noteUIInstance);
        noteHandler.SetNoteUIActive(false);
    }

    void DeleteDot(){
        Destroy(noteUIInstance);
        noteHandler.SetNoteUIActive(false);
        Destroy(gameObject);
    }

    void SaveName(){
        noteHandler.NoteName.gameObject.SetActive(true);
        noteHandler.NoteName.text = noteHandler.NoteNameField.text;
        noteHandler.NoteNameField.gameObject.SetActive(false);
        noteHandler.saveButton.gameObject.SetActive(false);
        noteHandler.editButton.gameObject.SetActive(true);
    }

    void EditDetails(){
        noteHandler.NoteName.gameObject.SetActive(false);
        noteHandler.NoteNameField.gameObject.SetActive(true);
        noteHandler.saveButton.gameObject.SetActive(true);
        noteHandler.editButton.gameObject.SetActive(false);
    }


}
