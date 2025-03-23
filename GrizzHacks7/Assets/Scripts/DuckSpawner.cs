using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DuckSpawner : MonoBehaviour {
    public GameObject prefab;
    public float spawnTime = 10.0f;
    private float nextSpawn = 0f;

    void Start()
    {
        nextSpawn = 5.0f;
    }

    public void Update()
    {
        nextSpawn += Time.deltaTime;

        if (nextSpawn > spawnTime) {
            GameObject projectile = Instantiate(prefab, transform.position, transform.rotation, null);
            nextSpawn = 0f;
        }
    }
}