using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sends actions between ItemViewControllers.
/// Packs ItemViewControllers parameters into Data objects
/// </summary>
public class ItemPresenter : MonoBehaviour
{
    /// <summary>
    /// ItemDescription with a prefab for default item attached
    /// </summary>
    public ItemDescription defaultItemDescription;
    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for button item attached
    /// </summary>
    public ItemDescription buttonItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for Image Slideshow Item attached
    /// </summary>
    public ItemDescription imageItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for Audio Player Item attached
    /// </summary>
    public ItemDescription audioItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for Pose Recognition item attached
    /// </summary>
    public ItemDescription poseItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for Speech to Text item attached
    /// </summary>
    /// 
    public ItemDescription sttItemDescription;
    /// <summary>
    /// ItemDescription with a prefab for Camera item attached
    /// </summary>
    public ItemDescription cameraItemDescription;

    /// <summary>
    /// ItemDescription with a prefab for WeightScaler item attached
    /// </summary>
    public ItemDescription weightScalerItemDescription;


    /// <summary>
    /// Required for serializing the item parameters
    /// </summary>
    public ItemService itemService;

    /// <summary>
    /// A list of itemViewControllers of items in the scene
    /// </summary>
    public Dictionary<string, ItemViewController> itemViewControllers;

    /// <summary>
    /// Will be removed when release
    /// </summary>
    public bool test = false;


    public ItemPresenter()
    {
        itemViewControllers = new Dictionary<string, ItemViewController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (test) TestMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Get an ItemViewController for the item in the scene
    /// </summary>
    /// <param name="itemID">the ID of an item to get the view controller</param>
    /// <returns></returns>
    public ItemViewController GetItemViewController(string itemID)
    {
        if (itemViewControllers.TryGetValue(itemID, out ItemViewController itemViewController))
        {
            return itemViewController;
        }
        return null;
    }

    /// <summary>
    /// Get the cached item data
    /// </summary>
    /// <param name="itemID">ID of the item</param>
    /// <returns></returns>
    public ItemData GetItemDatabyID(string itemID)
    {
        return itemService.GetItemDataByID(itemID);
    }

    private void SetItemTransform(string itemID, Transform transform)
    {
        ItemViewController itemViewController = GetItemViewController(itemID);
        itemViewController.SetItemTransform(transform);
    }

    /// <summary>
    /// Get location, rotation and local scale
    /// </summary>
    /// <param name="itemID">ID of the item</param>
    /// <returns></returns>
    private Transform GetItemTransform(string itemID)
    {
        ItemViewController itemViewController = GetItemViewController(itemID);
        return itemViewController.GetItemTransform();
    }

    /// <summary>
    /// Cache the transform
    /// </summary>
    /// <param name="itemID">ID of the item</param>
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


    // TODO: Move partly into ItemViewController (ex. gettext, getstate etc)
    /// <summary>
    /// Get the parameters of Items from ItemViewControllers and cache them
    /// </summary>
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
            itemData.isTurnedON = ((LightItemViewController)GetItemViewController(itemData.itemID)).itemLight.enabled;
            itemData.colorIndex = ((LightItemViewController)GetItemViewController(itemData.itemID)).colorIndex;
        }

        foreach (ButtonItemData itemData in itemService.GetItems().buttonItemsData)
        {
            itemData.type = 0;
        }

        foreach (WeightScalerItemData itemData in itemService.GetItems().weightScalerItemsData)
        {
            itemData.requiredWeight = ((WeightScalerItemViewController)GetItemViewController(itemData.itemID)).GetRequredWeight();
        }
    }


    // TODO: consider put all the ItemDescriptions into one file
    /// <summary>
    /// Create item based on itemDescription. Cache it using itemService
    /// </summary>
    /// <param name="item">What kind of item to create</param>
    /// <returns></returns>
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

    /// <summary>
    /// Load the cached item
    /// </summary>
    /// <param name="itemData"></param>
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

    /// <summary>
    /// Load the cached actionData
    /// </summary>
    /// <param name="actionData"></param>
    public void LoadActionToScene(ActionData actionData)
    {
        GetItemViewController(actionData.senderID).CreateNewOrUpdateExistingSenderMethod(actionData);
    }


    /// <summary>
    /// Instantiate an item at selected pose and using the cached itemdata
    /// </summary>
    /// <param name="pose">The pose to instantiate item at</param>
    /// <param name="itemData">itemdata to be used. The position from itemdata won't be used</param>
    /// <returns></returns>
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
                    instantiatedItem.GetComponent<LightItemViewController>().SetColor(((LightItemData)itemData).colorIndex);
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
            case ItemType.POSE:
                {
                    instantiatedItem = Instantiate(poseItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
            case ItemType.STT:
                {
                    instantiatedItem = Instantiate(sttItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
            case ItemType.CAMERA:
                {
                    instantiatedItem = Instantiate(cameraItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
            case ItemType.WEIGHTSCALER:
                {
                    instantiatedItem = Instantiate(weightScalerItemDescription.itemPrefab, storedPosition, storedRotation);
                    instantiatedItem.GetComponent<WeightScalerItemViewController>().SetRequiredWeight(((WeightScalerItemData)itemData).requiredWeight);
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

    /// <summary>
    /// Load all the cached items and actions into the scene
    /// </summary>
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

    //private void RunAction(ActionData actionData)
    //{
    //    GetItemViewController(actionData.receiverID).CallMethod(actionData.receiverMethod, actionData.receiverArgs.Cast<object>().ToArray());
    //}

    // TODO: Move to unitTests
    private void TestMethod()
    {
        GameObject light = CreateItem(lightItemDescription);
        light.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.6f, 0.77f, 0.7f));
        GameObject button = CreateItem(buttonItemDescription);
        button.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.6f, 0.785f, 0.2f));
        //GameObject pose = CreateItem(poseItemDescription);
        //pose.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 0.8f, 0.2f));
        //GameObject stt = CreateItem(sttItemDescription);
        //stt.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 1.08f, 0.32f));
        //GameObject camera = CreateItem(cameraItemDescription);
        //camera.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 0.8f, 0.2f));
        GameObject weightScaler = CreateItem(weightScalerItemDescription);
        weightScaler.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 1.0f, 0.2f));


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

    /// <summary>
    /// Load an ActionData into the scene and cache it
    /// </summary>
    /// <param name="actionData"></param>
    public void AddActionData(ActionData actionData)
    {
        GetItemViewController(actionData.senderID).CreateNewOrUpdateExistingSenderMethod(actionData);
        itemService.AddActionData(actionData);
    }

}
