using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static DwellerLogic;
using TMPro;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using System.Collections;

public class DwellerManager : MonoBehaviour
{
    public static GameObject Instance;

    [SerializeField] private GameObject dialog, dialogBox;

    [SerializeField] private List<GameObject> dwellers; // List of all dwellers
    private List<string> dwellersNames; // List of all dwellers
    [SerializeField] private GameObject playerDweller;  // The player's dweller

    [SerializeField] public string playerVote;   // To store the player's vote
    private bool hasPlayerVoted = false, hasPlayerWorked = false, hasPlayerDrank = false; // Flag to ensure the player votes first
    [SerializeField] public string mostVoted;
    public Dictionary<string, int> voteCounts = new Dictionary<string, int>();

    [SerializeField] private int playersAlive;
    [SerializeField] private float messageDuration;

    private IEnumerator DisplayDialogRoutine(string message, float duration)
    {
        var textComponent = dialog.GetComponent<TextMeshProUGUI>();

        // Enable the dialog objects
        dialog.SetActive(true);
        dialogBox.SetActive(true);

        // Set the text message
        textComponent.text = message;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Hide the dialog objects
        dialog.SetActive(false);
        dialogBox.SetActive(false);
    }

    public int getPlayersAlive()
    {
        return playersAlive;
    }

    public GameObject getPlayer()
    {
        return playerDweller;
    }

    public List<GameObject> getDwellers()
    {
        return dwellers;
    }

    public int getVotes(string target)
    {
        foreach (int count in voteCounts.Values)
        {
            Debug.Log($"{count}");
        }
        return voteCounts[target];
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null && Instance != gameObject)
        {
            playerVote = Instance.GetComponent<DwellerManager>().playerVote;
            mostVoted = Instance.GetComponent<DwellerManager>().mostVoted;
            hasPlayerWorked = Instance.GetComponent<DwellerManager>().hasPlayerWorked;
            hasPlayerDrank = Instance.GetComponent<DwellerManager>().hasPlayerDrank;
            voteCounts = new Dictionary<string, int>(Instance.GetComponent<DwellerManager>().voteCounts);
            playersAlive = Instance.GetComponent<DwellerManager>().getPlayersAlive();
            messageDuration = Instance.GetComponent<DwellerManager>().messageDuration;
            foreach (string name in voteCounts.Keys)
            {
                Debug.Log($"{name}");
            }
            Destroy(Instance);
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {

    }

    void Update()
    {
        // Update gameplay logic if needed
    }

    public void SetPlayerVote(GameObject target)
    {
        // Ensure the target is valid and not the player themselves
        if (target != playerDweller)
        {
            playerVote = target.GetComponent<DwellerLogic>().getDweller().Name;
            hasPlayerVoted = true;
        }
        else
        {
            Debug.LogWarning("Player cannot vote for themselves!");
        }
    }

    public void VoteSession()
    {
        voteCounts = new Dictionary<string, int>();

        if (!hasPlayerVoted)
        {
            Debug.LogWarning("Player has not voted yet. Voting session cannot proceed.");
            return;
        }

        if (dwellers == null || dwellers.Count == 0)
        {
            Debug.LogWarning("No dwellers available for voting.");
        }

        // Add the player's vote to the tally
        if (playerVote != null)
        {
            if (voteCounts.ContainsKey(playerVote))
            {
                voteCounts[playerVote]++;
            }
            else
            {
                voteCounts[playerVote] = 1;
            }
        }

        // Gather votes from all other dwellers except the player
        foreach (GameObject dwellerObject in dwellers)
        {
            if (dwellerObject == playerDweller || dwellerObject == null) continue;

            Dweller dweller = dwellerObject.GetComponent<DwellerLogic>().getDweller();
            if (dweller != null && dweller.isAlive)
            {
                GameObject votedAgainst = dweller.vote();
                if (votedAgainst != null)
                {
                    if (voteCounts.ContainsKey(votedAgainst.GetComponent<DwellerLogic>().getDweller().Name))
                    {
                        voteCounts[votedAgainst.GetComponent<DwellerLogic>().getDweller().Name]++;
                    }
                    else
                    {
                        voteCounts[votedAgainst.GetComponent<DwellerLogic>().getDweller().Name] = 1;
                    }
                }
            }
        }

        foreach (GameObject dwellerObject in dwellers)
        {
            if (!voteCounts.ContainsKey(dwellerObject.GetComponent<DwellerLogic>().getDweller().Name))
            {
                voteCounts[dwellerObject.GetComponent<DwellerLogic>().getDweller().Name] = 0;
            }
        }

        Debug.LogWarning(voteCounts.Keys.ToString());
        // Determine the most voted person
        mostVoted = voteCounts
            .OrderByDescending(vote => vote.Value)
            .FirstOrDefault().Key;

        string verdict = "";
        // Log the results for debugging
        Debug.Log("Voting results:");
        foreach (var vote in voteCounts)
        {
            verdict += $"{vote} received {vote.Value} votes.\n";
        }

        if (mostVoted != null)
        {
            verdict += $"{mostVoted} is the most voted person.\n";
        }
        else
        {
            Debug.Log("No votes were cast.");
        }
        Debug.Log(verdict);
        DwellerLogic.dwellersByName[mostVoted].GetComponent<DwellerLogic>().getDweller().isAlive = false;
        playersAlive--;
        hasPlayerWorked = false;
        hasPlayerDrank = false;
    }

    public void bar()
    {
        if (!hasPlayerDrank)
        {
            foreach (GameObject dwellerObject in dwellers)
            {
                if (dwellerObject == playerDweller || dwellerObject == null) continue;

                if (dwellerObject != null)
                {
                    dwellerObject.GetComponent<DwellerLogic>().drink(playerDweller);
                }
            }
            hasPlayerDrank = true;
            StartCoroutine(DisplayDialogRoutine("You raised a toast to your comrades!", messageDuration));

        }
        else
        {
            StartCoroutine(DisplayDialogRoutine("You have drank enoguh. Come here after the vote.", messageDuration));
        }
    }

    public void work()
    {
        if(!hasPlayerWorked)
        {
            foreach (GameObject dwellerObject in dwellers)
            {
                if (dwellerObject == playerDweller || dwellerObject == null) continue;

                if (dwellerObject != null)
                {
                    dwellerObject.GetComponent<DwellerLogic>().work(playerDweller);
                }
            }
            hasPlayerWorked = true;
            StartCoroutine(DisplayDialogRoutine("You work hard for the glory of the union!", messageDuration));
        }
        else
        {
            StartCoroutine(DisplayDialogRoutine("You have worked hard enoguh. Come here after the vote.", messageDuration));
        }
    }

    public void befriend(GameObject friend)
    {
        friend.GetComponent<DwellerLogic>().getDweller().friendly++;
        friend.GetComponent<DwellerLogic>().getDweller().updateRelationship(playerDweller, 10);

        foreach (GameObject dwellerObject in dwellers)
        {
            if (dwellerObject == playerDweller || dwellerObject == friend || dwellerObject == null) continue;

            Dweller dweller = dwellerObject.GetComponent<DwellerLogic>().getDweller();

            if (dweller != null)
            {
                if (dweller.getRelationship(friend) < 50)
                {
                    dweller.updateRelationship(playerDweller, -10);
                }
                else
                {
                    dweller.updateRelationship(playerDweller, 10);
                }
            }
        }
    }

    public void rumor(GameObject target)
    {
        foreach (GameObject dwellerObject in dwellers)
        {
            if (dwellerObject == playerDweller || dwellerObject == target || dwellerObject == null) continue;

            Dweller dweller = dwellerObject.GetComponent<DwellerLogic>().getDweller();

            if (dweller != null)
            {
                dweller.updateRelationship(target, -10);
            }
        }
    }
}
