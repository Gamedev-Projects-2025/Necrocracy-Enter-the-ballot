using UnityEngine;

[System.Serializable]
public class DialogAction
{
    public string ActionName;
    public GameObject TargetObject;

    public void PerformAction()
    {
        if (TargetObject != null)
        {
            // Example: Call a method on the target object based on ActionName
            TargetObject.SendMessage(ActionName, SendMessageOptions.DontRequireReceiver);
        }
    }
}
