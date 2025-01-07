using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogTree
{
    public List<DialogNode> Nodes = new List<DialogNode>();

    public DialogNode GetNode(int nodeID)
    {
        return Nodes.Find(node => node.ID == nodeID);
    }
}