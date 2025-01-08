using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dweller
{
    [Header("Dweller Information")]
    public string Name;
    public bool isAlive = true;
    public int alcoholTolerance = 1; // 1 = low, 5 = high
    public int hardWorking = 1; // 1 = lazy, 5 = very hard-working
    public int friendly = 1; // 1 = unfriendly, 5 = very friendly
    public int hostile = 1; // 1 = not hostile, 5 = very hostile
    public Sprite portrait;
    public int currentDialogNodeID = 0; // Tracks the current position in the dialog tree


    [Header("Relationships")]
    [SerializeField]
    [Tooltip("List of relationships with other dwellers.")]
    public List<RelationshipEntry> relationshipEntries = new List<RelationshipEntry>();
    public Dictionary<string, int> relationshipScoresByName = new Dictionary<string, int>();

    [Header("Dialog System")]
    [SerializeField]
    [Tooltip("Dialog tree for the dweller.")]
    public DialogTree dialogTree;


    public void updateRelationship(GameObject target, int change)
    {
        if (target == null) return;

        RelationshipEntry entry = relationshipEntries.Find(e => e.Target == target);

        if (entry != null)
        {
            entry.Score += change;
            relationshipScoresByName[target.GetComponent<DwellerLogic>().getDweller().Name] += change;
        }
        else
        {
            relationshipEntries.Add(new RelationshipEntry(target, change));
            if (!relationshipScoresByName.ContainsKey(target.GetComponent<DwellerLogic>().getDweller().Name))
            {
                relationshipScoresByName.Add(target.GetComponent<DwellerLogic>().getDweller().Name, change);

            }
        }
    }

    public void setRelationship(GameObject target, int change)
    {
        if (target == null) return;

        RelationshipEntry entry = relationshipEntries.Find(e => e.Target == target);

        if (entry != null)
        {
            entry.Score = change;
            relationshipScoresByName[target.GetComponent<DwellerLogic>().getDweller().Name] = change;
        }
    }

    public void loadRelationShips()
    {
        foreach (RelationshipEntry dwellerEntry in relationshipEntries)
        {
            setRelationship(dwellerEntry.Target, relationshipScoresByName[dwellerEntry.Target.GetComponent<DwellerLogic>().getDweller().Name]);
        }
    }

    public int getRelationship(GameObject target)
    {
        if (target == null) return 0;

        RelationshipEntry entry = relationshipEntries.Find(e => e.Target == target);
        return entry != null ? entry.Score : 0;
    }

    public GameObject vote()
    {
        GameObject worstDweller = null;
        int worstScore = int.MaxValue;

        foreach (var entry in relationshipEntries)
        {
            if (entry.Target != null)
            {
                Dweller targetDweller = entry.Target.GetComponent<DwellerLogic>().getDweller();
                if (targetDweller != null && targetDweller.isAlive && entry.Score < worstScore)
                {
                    worstScore = entry.Score;
                    worstDweller = entry.Target;
                }
            }
        }

        return worstDweller;
    }

    // Dialog-related methods
    public DialogNode GetCurrentDialogNode()
    {
        return dialogTree != null ? dialogTree.GetNode(currentDialogNodeID) : null;
    }

    public void AdvanceDialog(int choiceIndex)
    {
        if (dialogTree == null) return;

        DialogNode currentNode = dialogTree.GetNode(currentDialogNodeID);

        if (currentNode != null && choiceIndex >= 0 && choiceIndex < currentNode.Choices.Count)
        {
            currentDialogNodeID = currentNode.Choices[choiceIndex].NextNodeID;
            currentNode.Choices[choiceIndex].PerformActions();
            Debug.Log("Action taken");
        }
    }
}