using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessVote : MonoBehaviour
{
    [SerializeField] private string gameover, victory, hall;
    [SerializeField] private GameObject manager;
    [SerializeField] private int minimalDwellers;
    private DwellerManager dwellerManager;

    private void OnEnable()
    {
        dwellerManager = manager.GetComponent<DwellerManager>();

        if (dwellerManager != null)
        {
            if (dwellerManager.getPlayersAlive() <= 2 && dwellerManager.getPlayer().GetComponent<DwellerLogic>().getDweller().isAlive)
            {
                resetDwellers();
                SceneManager.LoadScene(victory);
            }
            else if (!dwellerManager.getPlayer().GetComponent<DwellerLogic>().getDweller().isAlive)
            {
                resetDwellers();
                SceneManager.LoadScene(gameover);
            }
            else
            {
                SceneManager.LoadScene(hall);
            }
        }
    }
    private void resetDwellers()
    {
        foreach (GameObject dweller in dwellerManager.getDwellers())
        { 
            Destroy(dweller);
        }
        Destroy(manager);
    }
}
