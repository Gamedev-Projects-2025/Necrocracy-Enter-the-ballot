using UnityEngine;

public class SpawnAndDestroyPrefab : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Inspector
    private GameObject spawnedPrefab; // Reference to the currently spawned prefab

    public void TogglePrefab()
    {
        if (spawnedPrefab == null)
        {
            // Spawn the prefab if it doesn't exist
            SpawnPrefab();
        }
        else
        {
            // Destroy the prefab if it exists
            DestroyPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // Instantiate the prefab
            spawnedPrefab = Instantiate(prefabToSpawn);

            // Parent it to the camera
            spawnedPrefab.transform.SetParent(Camera.main.transform);

            // Set position to the middle of the screen (camera's position)
            spawnedPrefab.transform.localPosition = new Vector3 (0, 0, -1);
            // Optionally reset rotation and scale
            spawnedPrefab.transform.localRotation = Quaternion.identity;
            spawnedPrefab.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogError("Prefab is not assigned!");
        }
    }

    private void DestroyPrefab()
    {
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
            spawnedPrefab = null;
        }
    }
}
