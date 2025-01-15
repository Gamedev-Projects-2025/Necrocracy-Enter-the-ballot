using UnityEngine;

public class EnableChoice
{
    public string dwellerName;
    public int nodeIndex, choiceIndex;

    public void enableChoice(string dwellerName, int nodeIndex, int choiceIndex)
    {
        DwellerLogic.dwellersByName[dwellerName].GetComponent<DwellerLogic>().getDweller().dialogTree.GetNode(nodeIndex).Choices[choiceIndex].isEnabled = true;
    }
    
}
