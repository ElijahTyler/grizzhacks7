using UnityEngine;

public class Roatation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        // Debug.Log(horizontal);

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }
}
