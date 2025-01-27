using UnityEngine;
using System.Reflection;
using System.Linq;

[System.Serializable]
public class DialogAction{

    public string dwellerName, optionalOriginDweller;
    public int change = 0;
    public bool hasHappened = false;
    public bool useOriginDweller = true, enableNewDialog = false;

    public int nodeIndex, choiceIndex;


    public void PerformAction()
    {
        if (!hasHappened)
        {
            if (useOriginDweller)
            {
                dialogManager.dweller.getDweller().updateRelationship(DwellerLogic.dwellersByName[dwellerName], change);
            }
            else
            {
                DwellerLogic.dwellersByName[optionalOriginDweller].gameObject.GetComponent<DwellerLogic>().getDweller().updateRelationship(DwellerLogic.dwellersByName[dwellerName], change);
            }

            Debug.Log(dialogManager.dweller.getDweller().getRelationship(DwellerLogic.dwellersByName[dwellerName]));
            hasHappened = true;

            if (enableNewDialog)
            {
                enableChoice();
            }
        }
    }

    public void enableChoice()
    {
        DwellerLogic.dwellersByName[dwellerName].GetComponent<DwellerLogic>().getDweller().dialogTree.GetNode(nodeIndex).Choices[choiceIndex].isEnabled = true;
    }
}
