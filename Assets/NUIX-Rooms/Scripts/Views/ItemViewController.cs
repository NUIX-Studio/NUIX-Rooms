using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



/// <summary>
/// Responsible for updating the Item GameObject
/// And tracking its changes
/// </summary>
public class ItemViewController : MonoBehaviour
{
    private ItemPresenter itemPresenter;

    private GameObject itemGameObject;

    public Vector3 SpawningAreaPosition;

    public string itemID;

    public List<string> senderMethods;
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
        senderMethods = new List<string>();
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

}

public class ButtonItemViewController: ItemViewController
{ 
    ButtonItemViewController()
    {
        senderMethods.Add(nameof(Press));
    }

    public void Press()
    {

    }
}
