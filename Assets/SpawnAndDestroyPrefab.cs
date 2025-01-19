using UnityEngine;

public class SpawnAndDestroyPrefab : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Inspector
    private GameObject spawnedPrefab; // Reference to the currently spawned prefab

    public void OnNoteButtonPressed()
    {
        // Spawn the prefab when the button is pressed
        if (spawnedPrefab == null && prefabToSpawn != null)
        {
            // Instantiate the prefab
            spawnedPrefab = Instantiate(prefabToSpawn);

            // Parent it to the camera
            spawnedPrefab.transform.SetParent(Camera.main.transform);

            // Set position to the middle of the screen (camera's position)
            spawnedPrefab.transform.localPosition = new Vector3(0, 0, 1);

            // Optionally reset rotation and scale
            spawnedPrefab.transform.localRotation = Quaternion.identity;
        }
    }

    public void OnNoteButtonReleased()
    {
        // Destroy the prefab when the button is released
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
            spawnedPrefab = null;
        }
    }
}
