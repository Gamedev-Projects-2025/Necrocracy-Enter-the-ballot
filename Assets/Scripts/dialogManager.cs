using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogManager : MonoBehaviour
{
    public static DwellerLogic dweller;
    public static string previousScene;
    [SerializeField] private GameObject returnObject;
    [SerializeField] private TextMeshProUGUI dialogTextBox;
    [SerializeField] private VerticalLayoutGroup choicesContainer;
    [SerializeField] public DwellerManager dwellerManager;

    public void Start()
    {
        dweller.getDweller().currentDialogNodeID = 0;
        // Set the portrait of the dweller
        gameObject.GetComponent<SpriteRenderer>().sprite = dweller.getDweller().portrait;

        // Set the return object's scene name
        returnObject.GetComponent<loadScene>().sceneName = previousScene;

        // Initialize the dialog box
        DialogNode currentNode = dweller.getDweller().dialogTree.GetNode(dweller.getDweller().currentDialogNodeID);
        if (currentNode != null)
        {
            SetDialog(currentNode);
        }
    }

    public void resetDialog()
    {
        dweller.getDweller().currentDialogNodeID = 0;
        DialogNode currentNode = dweller.getDweller().dialogTree.GetNode(dweller.getDweller().currentDialogNodeID);
        if (currentNode != null)
        {
            SetDialog(currentNode);
        }
    }
    private void SetDialog(DialogNode node)
    {
        // Set the dialog text
        dialogTextBox.text = node.Text;

        // Clear existing choices
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Create UI buttons for choices
        for (int i = 0; i < node.Choices.Count; i++)
        {
            DialogChoice choice = node.Choices[i];
            if (choice.isEnabled)
            {
                // Create a new button
                GameObject choiceButton = new GameObject("ChoiceButton");
                choiceButton.transform.SetParent(choicesContainer.transform);
                CanvasRenderer buttonCanvasRenderer = choiceButton.AddComponent<CanvasRenderer>();
                Image buttonImage = choiceButton.AddComponent<Image>(); // Image component

                // Add a Button component
                Button buttonComponent = choiceButton.AddComponent<Button>();

                // Create and add a TextMeshProUGUI component for the button's label
                GameObject textObject = new GameObject("ButtonText");
                textObject.transform.SetParent(choiceButton.transform);
                TextMeshProUGUI buttonText = textObject.AddComponent<TextMeshProUGUI>();
                buttonText.text = choice.Text;

                // Set text color to black and adjust font size
                buttonText.color = Color.white;
                buttonText.alignment = TextAlignmentOptions.Center;
                buttonText.fontSize = 36; // Set a smaller font size that fits in the button

                // RectTransform for the button
                RectTransform buttonRectTransform = choiceButton.GetComponent<RectTransform>();
                buttonRectTransform.sizeDelta = new Vector2(1000, 80);
                buttonRectTransform.localScale = Vector3.one; // Ensure the scale is 1
                buttonRectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Center the button
                buttonRectTransform.anchorMax = new Vector2(0.5f, 0.5f); // Center the button

                // Set up image background (optional)
                buttonImage.color = new Color(0.2f, 0.2f, 0.2f, 1f); // Dark background (optional)
                buttonImage.type = Image.Type.Sliced; // Make sure the image is sliced for proper scaling

                // RectTransform for text
                RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
                textRectTransform.sizeDelta = buttonRectTransform.sizeDelta; // Match the size of the button
                textRectTransform.localPosition = Vector3.zero; // Center the text within the button

                // Button onClick event
                int choiceIndex = i; // Capture index for the lambda
                buttonComponent.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        DialogNode currentNode = dweller.getDweller().dialogTree.GetNode(dweller.getDweller().currentDialogNodeID);
        if (currentNode == null) return;

        DialogChoice selectedChoice = currentNode.Choices[choiceIndex];
        selectedChoice.PerformActions();
        dweller.getDweller().currentDialogNodeID = selectedChoice.NextNodeID;

        DialogNode nextNode = dweller.getDweller().dialogTree.GetNode(selectedChoice.NextNodeID);
        if (nextNode != null)
        {
            SetDialog(nextNode);
        }
        else
        {
            // Dialog has ended; handle any cleanup or transition here
            Debug.Log("Dialog has ended.");
        }
    }
}
