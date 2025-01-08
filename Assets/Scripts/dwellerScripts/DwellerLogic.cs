using System.Collections.Generic;
using UnityEngine;

public class DwellerLogic : MonoBehaviour
{
    public static readonly Dictionary<string, GameObject> dwellersByName = new Dictionary<string, GameObject>();

    [Header("Dweller Instance")]
    [SerializeField] private Dweller dweller;

    public Dweller getDweller()
    {
        return dweller;
    }

    public void drink(GameObject target)
    {
        int change = dweller.alcoholTolerance switch
        {
            1 => -10 * dweller.hostile,
            2 => -5 * dweller.hostile,
            3 => 0,
            4 => 5 * dweller.friendly,
            5 => 10 * dweller.friendly,
            _ => 0
        };
        dweller.updateRelationship(target, change);
    }

    public void work(GameObject target)
    {
        int change = dweller.hardWorking switch
        {
            1 => -10 * dweller.hostile,
            2 => -5 * dweller.hostile,
            3 => 0,
            4 => 5 * dweller.friendly,
            5 => 10 * dweller.friendly,
            _ => 0
        };
        dweller.updateRelationship(target, change);
    }
    public void Start()
    {
        foreach (RelationshipEntry entry in dweller.relationshipEntries)
        {
            if (!dweller.relationshipScoresByName.ContainsKey(entry.Target.GetComponent<DwellerLogic>().getDweller().Name))
            {
                dweller.relationshipScoresByName.Add(entry.Target.GetComponent<DwellerLogic>().getDweller().Name, entry.Score);
            }
            else
            {
                dweller.relationshipScoresByName[entry.Target.GetComponent<DwellerLogic>().getDweller().Name] = entry.Score;
            }
        }
    }

    private void Awake()
    {
        // Check for duplicates and handle data transfer
        GameObject existingInstance = FindExistingInstance(dweller.Name);
        if (existingInstance != null && existingInstance != this)
        {
            existingInstance.GetComponent<DwellerLogic>().TransferDataToNewInstance(this);
            
            Destroy(existingInstance); // Destroy the old instance
            dwellersByName[dweller.Name] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Mark this as persistent
            dwellersByName[dweller.Name] = gameObject; // Register in the dictionary
        }
    }

    public GameObject FindExistingInstance(string name)
    {
        
        if (dwellersByName.TryGetValue(name, out GameObject existing))
        {
            return existing;
        }
        return null;
    }

    private void TransferDataToNewInstance(DwellerLogic newInstance)
    {
        newInstance.dweller.isAlive = dweller.isAlive;
        newInstance.dweller.alcoholTolerance = dweller.alcoholTolerance;
        newInstance.dweller.hardWorking = dweller.hardWorking;
        newInstance.dweller.friendly = dweller.friendly;
        newInstance.dweller.hostile = dweller.hostile;
        newInstance.dweller.portrait = dweller.portrait;
        newInstance.dweller.currentDialogNodeID = dweller.currentDialogNodeID;
        newInstance.dweller.dialogTree = dweller.dialogTree;


        // Transfer relationships
        foreach (string key in dweller.relationshipScoresByName.Keys)
        {
            newInstance.dweller.relationshipScoresByName[key] = dweller.relationshipScoresByName [key];

        }
        newInstance.dweller.loadRelationShips();
    }
}
