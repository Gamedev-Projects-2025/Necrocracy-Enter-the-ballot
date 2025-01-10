using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginDialog : MonoBehaviour
{
    [SerializeField] private string dialogScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadDialog(DwellerLogic dweller)
    {
        dialogManager.dweller = dweller;
        dialogManager.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(dialogScene);
    }
}
