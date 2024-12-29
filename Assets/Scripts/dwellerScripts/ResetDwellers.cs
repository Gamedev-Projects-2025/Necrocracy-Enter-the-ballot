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
        foreach (GameObject dweller in dwellerManager.getDwellers())
        {
            Destroy(dweller);
        }
        Destroy(manager);
    }
}
