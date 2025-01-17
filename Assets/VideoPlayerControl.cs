using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerControl : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public string videoFileName = "myFile.mp4"; // Name of the video file in StreamingAssets

    void Start()
    {
        // Combine the StreamingAssets path with the video file name
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

        // Set the video URL
        videoPlayer.url = videoPath;

        // Optional: Automatically play the video when the scene starts
        videoPlayer.Play();
    }

    // Function to play the video
    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Function to pause the video
    public void PauseVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    // Function to stop the video
    public void StopVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}
