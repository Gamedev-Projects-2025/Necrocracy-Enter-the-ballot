using TMPro;
using UnityEngine;

public class SetEnding : MonoBehaviour
{
    [SerializeField] private DwellerManager dwellerManager;
    [SerializeField] private TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string dead = "wife";
        foreach(GameObject dweller in dwellerManager.getDwellers())
        {
            if (dweller.GetComponent<DwellerLogic>().getDweller().Name != "player" && dweller.GetComponent<DwellerLogic>().getDweller().isAlive)
            {
                dead = dweller.GetComponent<DwellerLogic>().getDweller().Name;
            }
        }
        switch (dead)
        {
            case "wife":
                text.text = "Ending 5/5 - The death of Mrs. Helianthus";
                break;
            case "husband":
                text.text = "Ending 4/5 - The death of Mr. Helianthus";
                break;
            case "old":
                text.text = "Ending 3/5 - The death of comrade Altman";
                break;
            case "young":
                text.text = "Ending 2/5 - The death of comrade Altman";
                break;
            default:
                text.text = "See you next time!";
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
