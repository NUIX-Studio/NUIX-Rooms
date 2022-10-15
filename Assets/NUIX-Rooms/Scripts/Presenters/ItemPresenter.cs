using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsible for sending the actions to connected GameObjects
/// Also implements the actions themselves
/// </summary>
public class ItemPresenter : MonoBehaviour
{
    public string itemID;
    public UnityEvent actions = new();

    public UnityEvent method;

    public ItemService itemService;

    private ItemViewController itemViewController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ItemData GetItemData()
    {
        return itemService.GetItemDataByID(itemID);
    }


    private void SetItemTransform(Transform transform)
    {
        itemViewController.SetItemTransform(transform);
    }
    
    private Transform GetItemTransform()
    {
        return itemViewController.GetItemTransform();
    }

    private void SaveItemTransform()
    {
        ItemData itemData = itemService.GetItemDataByID(itemID);
        Transform itemTransform = GetItemTransform();
        itemData.position_x = itemTransform.position.x;
        itemData.position_y = itemTransform.position.y;
        itemData.position_z = itemTransform.position.z;

        itemData.rotation_x = itemTransform.rotation.x;
        itemData.rotation_y = itemTransform.rotation.y;
        itemData.rotation_z = itemTransform.rotation.z;
        itemData.rotation_w = itemTransform.rotation.w;
    }
}
