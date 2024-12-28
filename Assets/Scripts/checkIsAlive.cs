using UnityEngine;

public class checkIsAlive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DwellerLogic dweller;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private bool destroyOnAlive = false;
    void Start()
    {
        if (dweller != null)
        {
            if ((!dweller.getDweller().isAlive && destroyOnDeath) || (dweller.getDweller().isAlive && destroyOnAlive))
            {
                Destroy(gameObject);
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
