using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsible for sending the actions to connected GameObjects
/// Also implements the actions themselves
/// </summary>
public class ItemPresenter : MonoBehaviour
{
    public ItemDescription defaultItemDescription;
    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;
    public ItemDescription buttonItemDescription;
    public ItemDescription imageItemDescription;
    public ItemDescription audioItemDescription;

    public ItemService itemService;


    public Dictionary<string, ItemViewController> itemViewControllers;


    public ItemPresenter()
    {
        itemViewControllers = new Dictionary<string, ItemViewController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        TestMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // TODO: implement
    public ItemViewController GetItemViewController(string itemID)
    {
        if (itemViewControllers.TryGetValue(itemID, out ItemViewController itemViewController))
        {
            return itemViewController;
        }
        return null;
    }

    public ItemData GetItemDatabyID(string itemID)
    {
        return itemService.GetItemDataByID(itemID);
    }

    private void SetItemTransform(string itemID, Transform transform)
    {
        ItemViewController itemViewController = GetItemViewController(itemID);
        itemViewController.SetItemTransform(transform);
    }
    
    private Transform GetItemTransform(string itemID)
    {
        ItemViewController itemViewController = GetItemViewController(itemID);
        return itemViewController.GetItemTransform();
    }

    private void SaveItemTransform(string itemID)
    {
        ItemData itemData = itemService.GetItemDataByID(itemID);
        Transform itemTransform = GetItemTransform(itemID);
        itemData.position_x = itemTransform.position.x;
        itemData.position_y = itemTransform.position.y;
        itemData.position_z = itemTransform.position.z;

        itemData.rotation_x = itemTransform.rotation.x;
        itemData.rotation_y = itemTransform.rotation.y;
        itemData.rotation_z = itemTransform.rotation.z;
        itemData.rotation_w = itemTransform.rotation.w;
    }


    // TODO: Move to ItemService
    public void RetrieveItemsParams()
    {
        // Consider replace the iteration through lists to enumerable.concat

        foreach (ItemData itemData in itemService.GetItems().ConcatItemsData())
        {
            SaveItemTransform(itemData.itemID);
        }


        foreach (TextPlateItemData itemData in itemService.GetItems().textPlateItemsData)
        {
            itemData.text = GetItemViewController(itemData.itemID).gameObject.GetComponent<TextPlateItem>().plateText.text;
            itemData.isKeyboardOpen = GetItemViewController(itemData.itemID).gameObject.transform.Find("Visuals/Keyboard").gameObject.activeInHierarchy;
        }

        foreach (LightItemData itemData in itemService.GetItems().lightItemsData)
        {
            itemData.isTurnedON = GetItemViewController(itemData.itemID).gameObject.GetComponentInChildren<Light>().enabled;
        }

        foreach (ButtonItemData itemData in itemService.GetItems().buttonItemsData)
        {
            itemData.type = 0;
        }
    }


    // Next several functions I would rather out into a separate Script
    // Because ItemPresenter should be attached to only one item


    // TODO: consider put all the ItemDescriptions into one file
    public GameObject CreateItem(ItemDescription item)
    {
        Quaternion i = Quaternion.identity;
        string itemID = Guid.NewGuid().ToString();
        itemService.AddItemData(new ItemData(item.itemType, 0, 0, 0,
                i.x, i.y, i.z, i.w, itemID));
        ItemData itemData = itemService.GetItemDataByID(itemID);

        Vector3 spawnPosition = new(0, 0.5f, -1);

        Pose pose = new(spawnPosition, Quaternion.identity);
        GameObject instantiatedItem = CreateItemGameObject(pose, itemData);
        return instantiatedItem;
    }

    public void LoadItemToScene(ItemData itemData)
    {
        Vector3 storedPosition = new(itemData.position_x, itemData.position_y, itemData.position_z);
        Quaternion storedRotation = new(itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w);

        Pose pose = new(storedPosition, storedRotation);
        if (!itemViewControllers.ContainsKey(itemData.itemID))
        {
            GameObject instantiatedItem = CreateItemGameObject(pose, itemData);
        }
        else
        {
            Debug.Log("Item " + itemData.itemID + " of type " + itemData.itemType + " already exists in the scene");
        }
    }

    public void LoadActionToScene(ActionData actionData)
    {
        GetItemViewController(actionData.senderID).UpdateSenderMethod(actionData);
    }


    public GameObject CreateItemGameObject(Pose pose, ItemData itemData)
    {
        GameObject instantiatedItem;

        Vector3 storedPosition = pose.position;
        Quaternion storedRotation = pose.rotation;

        switch (itemData.itemType)
        {
            case ItemType.TEXTPLATE:
                {
                    instantiatedItem = Instantiate(textPlateItemDescription.itemPrefab, storedPosition, storedRotation);
                    instantiatedItem.GetComponent<TextPlateItem>().plateText.text = ((TextPlateItemData)itemData).text;
                    instantiatedItem.transform.Find("Visuals/Keyboard").gameObject.SetActive(((TextPlateItemData)itemData).isKeyboardOpen);
                    break;
                }
            case ItemType.LIGHT:
                {
                    instantiatedItem = Instantiate(lightItemDescription.itemPrefab, storedPosition, storedRotation);
                    instantiatedItem.GetComponentInChildren<Light>().enabled = ((LightItemData)itemData).isTurnedON;
                    break;
                }
            case ItemType.BUTTON:
                {
                    instantiatedItem = Instantiate(buttonItemDescription.itemPrefab, storedPosition, storedRotation);
                    // change button type
                    break;
                }
            case ItemType.IMAGE:
                {
                    instantiatedItem = Instantiate(imageItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
            case ItemType.AUDIO:
                {
                    instantiatedItem = Instantiate(audioItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
            default:
                {
                    instantiatedItem = Instantiate(defaultItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
        }
        instantiatedItem.GetComponent<ItemViewController>().itemID = itemData.itemID;
        instantiatedItem.GetComponent<ItemViewController>().itemPresenter = this;
        itemViewControllers.Add(itemData.itemID, instantiatedItem.GetComponent<ItemViewController>());
        return instantiatedItem;

    }


    public void AddItemsToScene()
    {
        foreach (ItemData itemData in itemService.GetItems().ConcatItemsData())
        {
            LoadItemToScene(itemData);
        }

        foreach (ActionData actionData in itemService.GetItems().actionData)
        {
            LoadActionToScene(actionData);
        }
    }

    private void RunAction(ActionData actionData)
    {
        GetItemViewController(actionData.receiverID).CallMethod(actionData.receiverMethod, actionData.receiverArgs.Cast<object>().ToArray());
    }

    // TODO: Move to unitTests
    private void TestMethod()
    {
        GameObject light = CreateItem(lightItemDescription);
        light.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.3f, 0.8f, 0.2f));
        GameObject button = CreateItem(buttonItemDescription);
        button.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 0.8f, 0.2f));
        //string actionID = Guid.NewGuid().ToString();
        //ActionData actionData = new ActionData(actionID,
        //    button.GetComponent<ItemViewController>().itemID,
        //    "Press",
        //    new List<string>(),
        //    light.GetComponent<ItemViewController>().itemID,
        //    "Toggle",
        //    new List<string>()
        //    );
        //button.GetComponent<ItemViewController>().UpdateSenderMethod(actionData);
        //itemService.AddActionData(actionData);
    }

    public void AddActionData(ActionData actionData)
    {
        GetItemViewController(actionData.senderID).UpdateSenderMethod(actionData);
        itemService.AddActionData(actionData);
    }

}
