using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    // List of objects to toggle
    public GameObject[] objectsToToggle;

    // Current toggle state
    private bool isToggledOn = false;

    // Toggle function to be called on button click
    public void Toggle()
    {
        isToggledOn = !isToggledOn; // Switch state

        // Toggle each object in the list
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(isToggledOn);
            }
        }

        Debug.Log($"Objects toggled {(isToggledOn ? "On" : "Off")}");
    }
}
