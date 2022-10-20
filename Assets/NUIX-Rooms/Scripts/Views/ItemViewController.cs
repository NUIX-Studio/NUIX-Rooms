using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;



/// <summary>
/// Responsible for updating the Item GameObject
/// And tracking its changes
/// </summary>
public class ItemViewController : MonoBehaviour
{

    public ItemPresenter itemPresenter;

    public Vector3 SpawningAreaPosition;

    public string itemID;

    public Dictionary <string, ActionData> senderMethods;
    public List<string> receiverMethods;


    


    void Start()
    {
        receiverMethods.Add(nameof(SetPosition));
        receiverMethods.Add(nameof(SetRotation));
        receiverMethods.Add(nameof(SetLocalScale));
    }


    public ItemViewController()
    {
        if (senderMethods == null) senderMethods = new Dictionary<string, ActionData>();
        if (receiverMethods == null) receiverMethods = new List<string>();

    }


    public Transform GetItemTransform()
    {
        return gameObject.transform;
    }

    public void SetItemTransform(Transform itemTransform)
    {
        SetPosition(itemTransform.position);
        SetRotation(itemTransform.rotation);
        SetLocalScale(itemTransform.localScale);
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        this.transform.rotation = rotation;
    }

    public void SetLocalScale(Vector3 localScale)
    {
        this.transform.localScale = localScale;
    }

    public void CallMethod(string method, object[] parameters)
    {
        try
        {
            MethodInfo methodInfo = this.GetType().GetMethod(method);
            methodInfo.Invoke(this, parameters);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void CallReceiverMethod(string senderMethod)
    {
        try
        {
            ActionData actionData = senderMethods[senderMethod];
            itemPresenter.GetItemViewController(actionData.receiverID).
                CallMethod(actionData.receiverMethod, actionData.receiverArgs.Cast<object>().ToArray());
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void CreateNewOrUpdateExistingSenderMethod(ActionData actionData)
    {
        if (actionData.actionID != null) senderMethods[actionData.senderMethod] = actionData;
    }
}
