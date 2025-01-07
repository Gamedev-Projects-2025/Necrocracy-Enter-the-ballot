using UnityEngine;

public class dialogManager: MonoBehaviour
{
    public static DwellerLogic dweller;
    public static string previousScene;
    [SerializeField] private GameObject returnObject;

    public void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = dweller.getDweller().portrait;
        returnObject.GetComponent<loadScene>().sceneName = previousScene;
    }
}
