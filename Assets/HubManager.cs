using UnityEngine;

public class HubManager : MonoBehaviour
{
    private void Start()
    {
        // Check if there is a saved camera position
        if (SaveLoadCameraPosition.Instance.HasSavedPosition())
        {
            Debug.Log("pose reset");
            // Restore the camera's position
            Camera.main.transform.position = SaveLoadCameraPosition.Instance.GetSavedCameraPosition();
        }
    }
}
