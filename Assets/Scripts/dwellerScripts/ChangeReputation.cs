using UnityEngine;

public class ChangeReputation : MonoBehaviour
{
    [SerializeField] private GameObject manager, target;
    [SerializeField] private int change;

    public void setRep()
    {
        foreach (GameObject dweller in manager.GetComponent<DwellerManager>().getDwellers())
        {
            dweller.GetComponent<DwellerLogic>().getDweller().updateRelationship(target, change);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
