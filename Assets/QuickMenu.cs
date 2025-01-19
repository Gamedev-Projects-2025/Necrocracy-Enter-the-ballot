using UnityEngine;
using UnityEngine.UI;

public class QuickMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject menuPanel; // The menu panel GameObject
    public Slider volumeSlider; // Slider to adjust volume
    public Button closeButton; // Button to close the menu

    private void Start()
    {
        // Ensure the menu is hidden at the start
        menuPanel.SetActive(false);

        // Load saved volume from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        volumeSlider.value = savedVolume;
        AdjustVolume(savedVolume);

        // Add listeners to UI elements
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        closeButton.onClick.AddListener(CloseMenu);
    }

    private void Update()
    {
        // Toggle menu visibility with a keypress (optional)
        if (Input.GetKeyDown(KeyCode.Escape)) // Or another key of your choice
        {
            ToggleMenu();
        }
    }

    // Adjust the volume and save the value
    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
    }

    // Toggle the menu visibility
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    // Close the menu
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }
}
