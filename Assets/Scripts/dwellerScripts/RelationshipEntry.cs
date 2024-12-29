using UnityEngine;
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