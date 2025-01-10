using UnityEngine;
using System.Reflection;
using System.Linq;

[System.Serializable]
public class DialogAction{

    public string dwellerName;
    public int change = 0;
    public bool hasHappened = false;

    public void PerformAction()
    {
        if (!hasHappened)
        {
            dialogManager.dweller.getDweller().updateRelationship(DwellerLogic.dwellersByName[dwellerName], change);

            Debug.Log(dialogManager.dweller.getDweller().getRelationship(DwellerLogic.dwellersByName[dwellerName]));
            hasHappened = true;
        }
    }
}
