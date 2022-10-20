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
        emptyAction.senderMethod = nameof(Press);
        emptyAction.senderID = itemID;
        CreateNewOrUpdateExistingSenderMethod(emptyAction);
    }

    public void Press()
    {
        CallReceiverMethod(nameof(Press));
        Debug.Log(itemID + " " + nameof(Press));
    }
}
