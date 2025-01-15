using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogChoice
{
    public string Text;
    public int NextNodeID;
    public List<DialogAction> Actions = new List<DialogAction>();
    public bool isEnabled = true;
    public void PerformActions()
    {
        foreach (var action in Actions)
        {
            action.PerformAction();
        }
    }
}
