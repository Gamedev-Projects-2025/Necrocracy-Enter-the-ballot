using UnityEngine;
using UnityEngine.Events;

public class ClickableSprite : MonoBehaviour
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public ButtonEvent onClick; // UnityEvent for handling method calls

    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer for visual effects
    private Vector3 originalScale;        // To restore the original size
    private Color originalColor;          // To restore the original color

    [SerializeField] private float sizeDown = 0.9f; // Scale factor for the click effect
    [SerializeField] private bool disableOnClick = false, isEnabled = true;

    void Start()
    {
        // Cache references
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        // Highlight the sprite on hover
        spriteRenderer.color = isEnabled? Color.green : Color.red;
    }

    private void OnMouseExit()
    {
        // Revert color when the mouse leaves
        spriteRenderer.color = originalColor;
    }

    private void OnMouseDown()
    {
        // Shrink the sprite slightly to show it's clicked
        transform.localScale = originalScale * sizeDown;
    }

    private void OnMouseUp()
    {
        // Restore the original scale when the mouse is released
        transform.localScale = originalScale;

        // Invoke the UnityEvent
        if (isEnabled)
        {
            onClick?.Invoke();
        }

        if (disableOnClick)
        {
            isEnabled = false;
        }
    }
}
