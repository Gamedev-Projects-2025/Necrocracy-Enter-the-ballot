using UnityEngine;

public class ResetDwellers : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    private DwellerManager dwellerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dwellerManager = manager.GetComponent<DwellerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetDwellers()
    {
        // Destroy all objects with DwellerLogic component
        DwellerLogic[] dwellersLogic = Object.FindObjectsByType<DwellerLogic>(FindObjectsSortMode.None);
        foreach (DwellerLogic dweller in dwellersLogic)
        {
            Destroy(dweller.gameObject);
        }

        // Destroy all objects with DwellerManager component (including the manager)
        DwellerManager[] managers = Object.FindObjectsByType<DwellerManager>(FindObjectsSortMode.None);
        foreach (DwellerManager dwManager in managers)
        {
            Destroy(dwManager.gameObject);
        }
    }
}
