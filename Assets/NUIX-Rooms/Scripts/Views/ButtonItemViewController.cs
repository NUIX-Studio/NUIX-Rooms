using UnityEngine;


/// <summary>
/// Attached to a ButtonDescriptionItem, adding extra actions
/// </summary>
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

    /// <summary>
    /// A sender method to be called when the button is pressed.
    /// Add it to OnClick component
    /// </summary>
    public void Press()
    {
        CallReceiverMethod(nameof(Press));
        Debug.Log(itemID + " " + nameof(Press));
    }
}
