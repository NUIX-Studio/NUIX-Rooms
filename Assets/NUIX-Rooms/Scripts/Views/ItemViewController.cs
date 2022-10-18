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

    // IS IT EVEN ADDED? NEED TO CHECK
    protected ItemPresenter itemPresenter;

    private GameObject itemGameObject;

    public Vector3 SpawningAreaPosition;

    public string itemID;

    public  Dictionary <string, ActionData> senderMethods;
    public List<string> receiverMethods;


    


    void Start()
    {
        if (GetComponent<ItemPresenter>() != null)
        {
            itemPresenter = GetComponent<ItemPresenter>();
        }
    }


    public ItemViewController()
    {
        senderMethods = new Dictionary<string, ActionData>();
        receiverMethods = new List<string>();
        receiverMethods.Add(nameof(SetPosition));
        receiverMethods.Add(nameof(SetRotation));
        receiverMethods.Add(nameof(SetLocalScale));
    }


    public Transform GetItemTransform()
    {
        return gameObject.transform;
    }

    public void SetItemTransform(Transform itemTransform)
    {
        if (itemGameObject != null)
        {
            SetPosition(itemTransform.position);
            SetRotation(itemTransform.rotation);
            SetLocalScale(itemTransform.localScale);
        }
    }

    public void SetPosition(Vector3 position)
    {
        if (itemGameObject != null)
        {
            itemGameObject.transform.position = position;
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        if (itemGameObject != null)
        {
            itemGameObject.transform.rotation = rotation;
        }
    }

    public void SetLocalScale(Vector3 localScale)
    {
        if (itemGameObject != null)
        {
            itemGameObject.transform.localScale = localScale;
        }
    }

    public void CallMethod(string method, object[] parameters)
    {
        try
        {
            MethodInfo methodInfo = this.GetType().GetMethod(method);
            methodInfo.Invoke(method, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.ReadKey();
        }
    }

    protected void CallReceiverMethod(string senderMethod)
    {
        ActionData actionData = senderMethods[senderMethod];
        itemPresenter.GetItemViewController(actionData.receiverID).CallMethod(actionData.receiverMethod, actionData.receiverArgs.Cast<object>().ToArray());
    }

}

public class ButtonItemViewController: ItemViewController
{ 
    ButtonItemViewController()
    {
        //senderMethods.Add(nameof(Press), null);
    }

    public void Press()
    {
        CallReceiverMethod(nameof(Press));
    }
}
