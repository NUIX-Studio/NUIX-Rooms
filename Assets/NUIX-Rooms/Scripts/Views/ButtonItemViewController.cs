using UnityEngine;

public class ButtonItemViewController: ItemViewController
{ 
    ButtonItemViewController()
    {
        //senderMethods.Add(nameof(Press), null);
    }

    public void Start()
    {
        // TODO move from start to constructor
        ActionData emptyAction = new ActionData();
        senderMethods.Add(nameof(Press), emptyAction);
    }

    public void Press()
    {
        CallReceiverMethod(nameof(Press));
        Debug.Log(itemID + " " + nameof(Press));
    }
}
