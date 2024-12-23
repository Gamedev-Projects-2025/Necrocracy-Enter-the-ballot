using UnityEngine;

public class checkIsAlive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DwellerLogic dweller;
    void Start()
    {
        if (dweller != null && !dweller.getDweller().isAlive)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
