using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour
{
    public GameObject leftButton;   // Assign the UI button for moving left
    public GameObject rightButton; // Assign the UI button for moving right
    public float moveSpeed = 0f;   // Speed of camera movement
    public float minX = 0f;      // Minimum X boundary
    public float maxX = 0f;       // Maximum X boundary

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the mouse is over the left button and held down
        if (IsMouseOverUIObject(leftButton) && Input.GetMouseButton(0))
        {
            MoveCamera(Vector3.left);
        }
        // Check if the mouse is over the right button and held down
        else if (IsMouseOverUIObject(rightButton) && Input.GetMouseButton(0))
        {
            MoveCamera(Vector3.right);
        }
    }

    private void MoveCamera(Vector3 direction)
    {
        // Calculate the new position
        Vector3 newPosition = mainCamera.transform.position + direction * moveSpeed * Time.deltaTime;

        // Clamp the new position within the boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Apply the new position to the camera
        mainCamera.transform.position = newPosition;
    }

    private bool IsMouseOverUIObject(GameObject uiObject)
    {
        // Use EventSystem to detect if the pointer is over the specific UI object
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == uiObject)
            {
                return true;
            }
        }

        return false;
    }
}
