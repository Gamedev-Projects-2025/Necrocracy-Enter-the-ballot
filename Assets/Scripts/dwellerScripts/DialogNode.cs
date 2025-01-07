using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogNode
{
    public int ID;
    public string Text;
    public List<DialogChoice> Choices = new List<DialogChoice>();
}
