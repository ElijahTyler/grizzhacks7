using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Duck : MonoBehaviour {
    public float speed = 1;

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}