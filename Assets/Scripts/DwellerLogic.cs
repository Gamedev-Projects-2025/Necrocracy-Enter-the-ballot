using System.Collections.Generic;
using UnityEngine;

public class DwellerLogic : MonoBehaviour
{
    public static readonly Dictionary<string, GameObject> dwellersByName = new Dictionary<string, GameObject>();
    [System.Serializable]
    public class RelationshipEntry
    {
        [Tooltip("The target Dweller GameObject.")]
        public GameObject Target; // GameObject reference for the related Dweller
        public int Score;

        public RelationshipEntry(GameObject target, int score)
        {
            Target = target;
            Score = score;
        }
    }

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

        [Header("Relationships")]
        [SerializeField]
        [Tooltip("List of relationships with other dwellers.")]
        public List<RelationshipEntry> relationshipEntries = new List<RelationshipEntry>();
        public Dictionary<string, int> relationshipScoresByName = new Dictionary<string, int>();

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
                relationshipScoresByName.Add(target.GetComponent<DwellerLogic>().getDweller().Name, change);
            }
        }
        public void setRelationship(GameObject target, int change)
        {
            if (target == null)
            {
                return;
            }
                

            RelationshipEntry entry = relationshipEntries.Find(e => e.Target == target);

            if (entry != null)
            {
                entry.Score = change;
                relationshipScoresByName[target.GetComponent<DwellerLogic>().getDweller().Name] = change;
            }

        }

        public void loadRelationShips()
        {
            foreach (RelationshipEntry dwellerEntey in relationshipEntries)
            {
                setRelationship(dwellerEntey.Target, relationshipScoresByName[dwellerEntey.Target.GetComponent<DwellerLogic>().getDweller().Name]);
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
    }

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

        // Transfer relationships
        foreach (string key in dweller.relationshipScoresByName.Keys)
        {
            newInstance.dweller.relationshipScoresByName[key] = dweller.relationshipScoresByName [key];

        }
        newInstance.dweller.loadRelationShips();
    }
}
