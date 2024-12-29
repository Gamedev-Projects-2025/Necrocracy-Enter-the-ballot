using TMPro;
using UnityEngine;

public class getVotes : MonoBehaviour
{
    [SerializeField] private DwellerManager dwellerManager;
    [SerializeField] private GameObject dweller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = dwellerManager.getVotes(dweller.GetComponent<DwellerLogic>().getDweller().Name).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
