using System.Collections;
using UnityEngine;

public class FlickeringTransparency : MonoBehaviour
{
    // Minimum and maximum transparency bounds
    [Range(0f, 1f)] public float minAlpha = 0.3f;
    [Range(0f, 1f)] public float maxAlpha = 1f;

    // The speed at which the transparency pulses
    public float flickerSpeed = 0.1f;

    // Reference to the object's SpriteRenderer
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer of the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the object. Make sure the object has a SpriteRenderer component.");
        }
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            // Randomly calculate a new alpha value within the bounds
            float randomAlpha = Random.Range(minAlpha, maxAlpha);

            // Lerp towards the new alpha value for smooth flickering
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a, randomAlpha, flickerSpeed * Time.deltaTime);

            // Apply the updated color to the SpriteRenderer
            spriteRenderer.color = color;
        }
    }
}
