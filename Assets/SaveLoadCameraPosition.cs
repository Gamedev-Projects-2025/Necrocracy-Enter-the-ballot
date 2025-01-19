using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadCameraPosition : MonoBehaviour
{
    public static SaveLoadCameraPosition Instance; // Singleton for global access

    private Vector3 savedCameraPosition; // Stores the camera's position
    private bool hasSavedPosition = false; // Tracks if a position has been saved

    private void Awake()
    {
        // Ensure there's only one instance of this script
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Save the camera's position
    public void SaveCameraPosition(Vector3 position)
    {
        savedCameraPosition = position;
        hasSavedPosition = true;
    }

    // Restore the camera's position if it was saved
    public Vector3 GetSavedCameraPosition()
    {
        return hasSavedPosition ? savedCameraPosition : Vector3.zero;
    }

    // Check if a position has been saved
    public bool HasSavedPosition()
    {
        return hasSavedPosition;
    }
}
