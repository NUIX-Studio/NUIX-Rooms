using Oculus.Interaction.Samples;
using Oculus.Platform.Samples.VrHoops;
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
    /// ItemDescription with a prefab for VideoPlayer Item attached
    /// </summary>
    public ItemDescription videoItemDescription;

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
    /// ItemDescription with a prefab for SceneLoader item attached
    /// </summary>
    public ItemDescription sceneLoaderItemDescription;


    /// <summary>
    /// Required for serializing the item parameters
    /// </summary>
    public ItemService itemService;

    /// <summary>
    /// A list of itemViewControllers of items in the scene
    /// </summary>
    public Dictionary<string, ItemViewController> itemViewControllers;


    [SerializeField] public bool itemsToCreate0;
    [SerializeField] public bool itemsToCreate1;
    [SerializeField] public bool itemsToCreate2;
    [SerializeField] public bool itemsToCreate3;
    [SerializeField] public bool itemsToCreate4;
    [SerializeField] public bool itemsToCreate5;

    private string userID;


    public ItemPresenter()
    {
        itemViewControllers = new Dictionary<string, ItemViewController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        if (itemsToCreate0) CreateItems0();
        if (itemsToCreate1) CreateItems1();
        if (itemsToCreate2) CreateItems2();
        if (itemsToCreate3) CreateItems3();
        if (itemsToCreate4) CreateItems4();
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

        foreach (var itemData in itemService.GetItems().ConcatItemsData())
        {
            var itemDataOld = itemData.DeepCopy();
            SaveItemTransform(itemData.itemID);
            itemData.userID = userID;
            if (!itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }


        foreach (TextPlateItemData itemData in itemService.GetItems().textPlateItemsData)
        {

            var itemDataOld = itemData.DeepCopy();
            

            itemData.text = ((TextPlateItemViewController)GetItemViewController(itemData.itemID)).PlateText;
            itemData.isKeyboardOpen = ((TextPlateItemViewController)GetItemViewController(itemData.itemID)).IsKeyBoardActive;


            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }

        }

        foreach (LightItemData itemData in itemService.GetItems().lightItemsData)
        {

            var itemDataOld = itemData.DeepCopy();


            itemData.isTurnedON = ((LightItemViewController)GetItemViewController(itemData.itemID)).itemLight.enabled;
            itemData.colorIndex = ((LightItemViewController)GetItemViewController(itemData.itemID)).colorIndex;


            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }

        }

        foreach (ButtonItemData itemData in itemService.GetItems().buttonItemsData)
        {
            var itemDataOld = itemData.DeepCopy();

            itemData.type = 0;

            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }

        foreach (ImageItemData itemData in itemService.GetItems().imageItemsData)
        {
            var itemDataOld = itemData.DeepCopy();


            itemData.imageIndex = ((ImageItemViewController)GetItemViewController(itemData.itemID)).imageIndex;

            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }

        foreach (VideoItemData itemData in itemService.GetItems().videoItemsData)
        {
            var itemDataOld = itemData.DeepCopy();

            itemData.videoClipIndex = ((VideoItemViewController)GetItemViewController(itemData.itemID)).videoClipIndex;

            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }

        foreach (AudioItemData itemData in itemService.GetItems().audioItemsData)
        {
            var itemDataOld = itemData.DeepCopy();

            itemData.audioClipIndex = ((AudioItemViewController)GetItemViewController(itemData.itemID)).audioClipIndex;

            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }

        foreach (WeightScalerItemData itemData in itemService.GetItems().weightScalerItemsData)
        {
            var itemDataOld = itemData.DeepCopy();

            itemData.requiredWeight = ((WeightScalerItemViewController)GetItemViewController(itemData.itemID)).GetRequredWeight();

            if (itemDataOld.JsonCompare(itemData))
            {
                int inUseTime = itemData.inUseTime;
                inUseTime = (inUseTime < 5) ? inUseTime : 5;
                itemData.inUseTime = (inUseTime > 0) ? inUseTime - 1 : 0;
            }
            else
            {
                itemData.inUseTime++;
            }
        }
    }


    // TODO: consider put all the ItemDescriptions into one file
    /// <summary>
    /// Create item based on itemDescription. Cache it using itemService
    /// </summary>
    /// <param name="item">What kind of item to create</param>
    /// <returns></returns>
    public GameObject CreateItem(ItemDescription item, Pose pose)
    {
        Quaternion i = Quaternion.identity;
        string itemID = Guid.NewGuid().ToString();
        itemService.AddItemData(new ItemData(item.itemType, 0, 0, 0,
                i.x, i.y, i.z, i.w, itemID));
        ItemData itemData = itemService.GetItemDataByID(itemID);

        GameObject instantiatedItem = CreateItemGameObject(pose, itemData);
        return instantiatedItem;
    }

    public void CreateItem(ItemDescription item)
    {
        CreateItem(item, new Pose(new Vector3(0f, 0.7f, 0f), Quaternion.identity));
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
            //itemViewControllers[itemData.itemID].SetPosition(storedPosition);
            //itemViewControllers[itemData.itemID].SetRotation(storedRotation);
            // TODO: update the parameters
            //if (itemData.userID == userID)
            UpdateItemGameObject(pose, itemData);
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
                    instantiatedItem.GetComponent<TextPlateItemViewController>().PlateText = ((TextPlateItemData)itemData).text;
                    instantiatedItem.GetComponent<TextPlateItemViewController>().IsKeyBoardActive = ((TextPlateItemData)itemData).isKeyboardOpen;
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
                    instantiatedItem.GetComponent<ImageItemViewController>().SetImage(((ImageItemData)itemData).imageIndex);
                    break;
                }
            case ItemType.VIDEO:
                {
                    instantiatedItem = Instantiate(videoItemDescription.itemPrefab, storedPosition, storedRotation);
                    instantiatedItem.GetComponent<VideoItemViewController>().SetVideoClip(((VideoItemData)itemData).videoClipIndex);
                    break;
                }
            case ItemType.AUDIO:
                {
                    instantiatedItem = Instantiate(audioItemDescription.itemPrefab, storedPosition, storedRotation);
                    instantiatedItem.GetComponent<AudioItemViewController>().SetAudioClip(((AudioItemData)itemData).audioClipIndex);
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
            case ItemType.SCENELOADER:
                {
                    instantiatedItem = Instantiate(sceneLoaderItemDescription.itemPrefab, storedPosition, storedRotation);
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
    /// Update an item at selected pose and using the cached itemdata
    /// </summary>
    /// <param name="pose">The pose to instantiate item at</param>
    /// <param name="itemData">itemdata to be used. The position from itemdata won't be used</param>
    /// <returns></returns>
    public void UpdateItemGameObject(Pose pose, ItemData itemData)
    {

        // Don't update because the item is currently operated by the user
        //if (itemData.inUseTime > 0) return;

        GameObject instantiatedItem = itemViewControllers[itemData.itemID].gameObject;

        // This is very time-ineffective function
        // We are going to iterate through all the gameobjects in the scene
        // Search for the gameobject whose itemviewcontroller itemid is equal to itemid
        // TODO: change gameobjects names to contain itemid.
        // Then just search for a gameobject by name, it will reduce complexity to contant time
        //var objects = gameobject.findgameobjectswithtag("item");
        //var objectcount = objects.length;
        //foreach (var obj in objects)
        //{
        //    continue;
        //}


        itemViewControllers[itemData.itemID].SetPosition(pose.position);
        itemViewControllers[itemData.itemID].SetRotation(pose.rotation);

        switch (itemData.itemType)
        {
            case ItemType.TEXTPLATE:
                {
                    instantiatedItem.GetComponent<TextPlateItemViewController>().PlateText = ((TextPlateItemData)itemData).text;
                    instantiatedItem.GetComponent<TextPlateItemViewController>().IsKeyBoardActive = ((TextPlateItemData)itemData).isKeyboardOpen;
                    break;
                }
            case ItemType.LIGHT:
                {
                    instantiatedItem.GetComponentInChildren<Light>().enabled = ((LightItemData)itemData).isTurnedON;
                    instantiatedItem.GetComponent<LightItemViewController>().SetColor(((LightItemData)itemData).colorIndex);
                    break;
                }
            case ItemType.BUTTON:
                {
                    // change button type
                    break;
                }
            case ItemType.IMAGE:
                {
                    instantiatedItem.GetComponent<ImageItemViewController>().SetImage(((ImageItemData)itemData).imageIndex);
                    break;
                }
            case ItemType.VIDEO:
                {
                    instantiatedItem.GetComponent<VideoItemViewController>().SetVideoClip(((VideoItemData)itemData).videoClipIndex);
                    break;
                }
            case ItemType.AUDIO:
                {
                    instantiatedItem.GetComponent<AudioItemViewController>().SetAudioClip(((AudioItemData)itemData).audioClipIndex);
                    break;
                }
            case ItemType.POSE:
                {
                    break;
                }
            case ItemType.STT:
                {
                    break;
                }
            case ItemType.CAMERA:
                {
                    break;
                }
            case ItemType.WEIGHTSCALER:
                {
                    instantiatedItem.GetComponent<WeightScalerItemViewController>().SetRequiredWeight(((WeightScalerItemData)itemData).requiredWeight);
                    break;
                }
            case ItemType.SCENELOADER:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        return;

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

    /// <summary>
    /// Load an ActionData into the scene and cache it
    /// </summary>
    /// <param name="actionData"></param>
    public void AddActionData(ActionData actionData)
    {
        GetItemViewController(actionData.senderID).CreateNewOrUpdateExistingSenderMethod(actionData);
        itemService.AddActionData(actionData);
    }


    public ItemData RetrieveItemDataFromGameObject(GameObject item)
    {
        return null;
    }

    public void CreateItems0()
    {
        GameObject pose = CreateItem(poseItemDescription, new Pose(new Vector3(-0.1f, 0.8f, 0.2f), Quaternion.identity));
        GameObject sceneLoader = CreateItem(sceneLoaderItemDescription, 
            new Pose(new Vector3(-0.532000005f, 0.787999988f, -0.130999997f), 
            new Quaternion(0.153045908f, -0.690345585f, 0.153045908f, 0.690345585f)));
        string actionID = Guid.NewGuid().ToString();
        ActionData actionData = new ActionData(actionID,
            pose.GetComponent<ItemViewController>().itemID,
            "ThumbsUpRight",
            new List<string>(),
            sceneLoader.GetComponent<ItemViewController>().itemID,
            "Load",
            new List<string> { "Room1-BtnCtrl" }
            );
        pose.GetComponent<ItemViewController>().CreateNewOrUpdateExistingSenderMethod(actionData);
        itemService.AddActionData(actionData);
    }

    public void CreateItems1()
    {
        GameObject light = CreateItem(lightItemDescription, new Pose(new Vector3(0f, 0.77f, 0.7f), Quaternion.identity));
        GameObject button = CreateItem(buttonItemDescription, new Pose(new Vector3(0f, 0.782f, 0.369f), Quaternion.identity));
        GameObject textplate = CreateItem(textPlateItemDescription, new Pose(new Vector3(0.1f, 0.782f, 0.269f), Quaternion.identity));
    }

    public void CreateItems2()
    {
        GameObject pose = CreateItem(poseItemDescription, new Pose(new Vector3(-0.1f, 0.8f, 0.2f), Quaternion.identity));
        GameObject image = CreateItem(imageItemDescription, new Pose(new Vector3(0f, 0.785f, 0.2f), Quaternion.identity));
    }

    public void CreateItems3()
    {
        GameObject stt = CreateItem(sttItemDescription, new Pose(new Vector3(0f, 1.08f, 0.32f), Quaternion.identity));
        GameObject light = CreateItem(lightItemDescription, new Pose(new Vector3(-0.6f, 0.77f, 0.7f), Quaternion.identity));
    }

    public void CreateItems4()
    {
        GameObject video = CreateItem(videoItemDescription, new Pose(new Vector3(-1.7052151f, 0.486999989f, 1.46103621f), new Quaternion(0, -0.386683226f, 0, 0.92221266f)));
        video.GetComponent<ItemViewController>().SetLocalScale(new Vector3(7.06168079f, 4.00269985f, 1));
        GameObject weightScaler = CreateItem(weightScalerItemDescription, new Pose(new Vector3(-0.1f, 0.8f, 0.2f), Quaternion.identity));
        weightScaler.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 1.0f, 0.2f));
    }
}
