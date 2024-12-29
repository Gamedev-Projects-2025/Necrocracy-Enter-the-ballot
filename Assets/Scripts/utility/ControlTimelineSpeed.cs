using UnityEngine;
using UnityEngine.Playables;

public class ControlTimelineSpeed : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector
    public float playSpeed = 1f; // Play speed multiplier (default: 1)

    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }

    void Update()
    {
        if (playableDirector != null)
        {
            // Adjust the time scale of the PlayableDirector
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(playSpeed);
        }
    }

    // Optional: A method to change speed dynamically via UI or other scripts
    public void SetPlaySpeed(float newSpeed)
    {
        playSpeed = newSpeed;
        if (playableDirector != null && playableDirector.time<0f)
        {
            playableDirector.time = 0f;
        }
    }
}
