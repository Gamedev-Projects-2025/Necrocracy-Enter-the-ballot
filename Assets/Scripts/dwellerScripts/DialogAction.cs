using UnityEngine;
using System.Reflection;
using System.Linq;

[System.Serializable]
public class DialogAction{

    public string dwellerName;
    public int change = 0;

    public void PerformAction()
    {
        dialogManager.dweller.getDweller().updateRelationship(DwellerLogic.dwellersByName[dwellerName], change);

        Debug.Log(dialogManager.dweller.getDweller().getRelationship(DwellerLogic.dwellersByName[dwellerName]));
        
    }
}
