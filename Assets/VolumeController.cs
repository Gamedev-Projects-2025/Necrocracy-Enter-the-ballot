using TMPro;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    // Step size for increasing/decreasing volume
    public float volumeStep = 0.1f;

    // Minimum and maximum volume levels
    private const float minVolume = 0f;
    private const float maxVolume = 1f;

    [SerializeField] private TextMeshProUGUI volumeText;

    private void Start()
    {
        volumeText.text = (Mathf.Round(AudioListener.volume * 100 / 10) * 10).ToString();
    }
    // Increases the volume
    public void VolumeUp()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume + volumeStep, minVolume, maxVolume);
        Debug.Log($"Volume increased to: {AudioListener.volume}");
        volumeText.text = (Mathf.Round(AudioListener.volume * 100 / 10) * 10).ToString();
    }

    // Decreases the volume
    public void VolumeDown()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume - volumeStep, minVolume, maxVolume);
        Debug.Log($"Volume decreased to: {AudioListener.volume}");
        volumeText.text = (Mathf.Round(AudioListener.volume * 100 / 10) * 10).ToString();
    }

    // Mutes the volume
    public void Mute()
    {
        AudioListener.volume = 0f;
        Debug.Log("Volume muted");
        volumeText.text = (Mathf.Round(AudioListener.volume * 100 / 10) * 10).ToString();
    }
}
