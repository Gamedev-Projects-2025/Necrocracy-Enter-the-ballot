using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        // Save the camera position before switching scenes
        SaveLoadCameraPosition.Instance.SaveCameraPosition(Camera.main.transform.position);
    }
}
