using UnityEngine;

public class VolumeController : MonoBehaviour
{
    // Step size for increasing/decreasing volume
    public float volumeStep = 0.1f;

    // Minimum and maximum volume levels
    private const float minVolume = 0f;
    private const float maxVolume = 1f;

    // Increases the volume
    public void VolumeUp()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume + volumeStep, minVolume, maxVolume);
        Debug.Log($"Volume increased to: {AudioListener.volume}");
    }

    // Decreases the volume
    public void VolumeDown()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume - volumeStep, minVolume, maxVolume);
        Debug.Log($"Volume decreased to: {AudioListener.volume}");
    }

    // Mutes the volume
    public void Mute()
    {
        AudioListener.volume = 0f;
        Debug.Log("Volume muted");
    }
}
