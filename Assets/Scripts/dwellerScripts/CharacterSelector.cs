using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    public Character[] characters; // Assign in the Inspector.
    public Image characterImage; // Reference to the UI Image.
    public TextMeshProUGUI nameText; // Reference to the name text.
    public TextMeshProUGUI bioText; // Reference to the bio text.
    private int currentIndex = 0;

    void Start()
    {
        DisplayCharacter();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        DisplayCharacter();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        DisplayCharacter();
    }

    private void DisplayCharacter()
    {
        if (characters.Length > 0)
        {
            Character currentCharacter = characters[currentIndex];
            characterImage.sprite = currentCharacter.portrait;
            nameText.text = currentCharacter.name;
            bioText.text = currentCharacter.bio;
        }
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
