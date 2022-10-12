using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemsStorage : MonoBehaviour
{
    public ItemsData itemsData;


    public ItemDescription defaultItemDescription;
    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;
    public ItemDescription buttonItemDescription;
    public ItemDescription imageItemDescription;
    public ItemDescription audioItemDescription;

    public Vector3 SpawningAreaPosition;

    private void CreateItemsData()
    {
        itemsData = new ItemsData();
    }

    private void Start()
    {
        CreateItemsData();
    }


    private void SetPositionAndRotationValuesForItemData(ItemData itemData)
    {
        itemData.position_x = itemData.gameObject.transform.position.x;
        itemData.position_y = itemData.gameObject.transform.position.y;
        itemData.position_z = itemData.gameObject.transform.position.z;

        itemData.rotation_x = itemData.gameObject.transform.rotation.x;
        itemData.rotation_y = itemData.gameObject.transform.rotation.y;
        itemData.rotation_z = itemData.gameObject.transform.rotation.z;
        itemData.rotation_w = itemData.gameObject.transform.rotation.w;
    }
    public void RetrieveItemsParams()
    {
        // Consider replace the iteration through lists to enumerable.concat

        foreach (ItemData itemData in itemsData.ConcatItemsData())
        {
            SetPositionAndRotationValuesForItemData(itemData);
        }

        foreach (TextPlateItemData itemData in itemsData.textPlateItemsData)
        {
            itemData.text = itemData.gameObject.GetComponent<TextPlateItem>().plateText.text;
            itemData.isKeyboardOpen = itemData.gameObject.transform.Find("Visuals/Keyboard").gameObject.activeInHierarchy;
        }

        foreach (LightItemData itemData in itemsData.lightItemsData)
        {
            itemData.isTurnedON = itemData.gameObject.GetComponentInChildren<Light>().enabled;
        }

        foreach (ButtonItemData itemData in itemsData.buttonItemsData)
        {
            itemData.type = 0;
        }

        foreach (ImageItemData itemData in itemsData.imageItemsData)
        {
            // does nothing
        }

        foreach (AudioItemData itemData in itemsData.audioItemsData)
        {
            // does nothing
        }
    }

    /// <summary>
    /// Instantiates an item 
    /// </summary>
    /// <param name="item"></param>
    public void CreateItem(ItemDescription item)
    {
        GameObject instantiatedItem = Instantiate(item.itemPrefab, SpawningAreaPosition, Quaternion.identity);
        ItemData itemData = new(item.itemType,
                instantiatedItem.transform.position.x, instantiatedItem.transform.position.y, instantiatedItem.transform.position.z,
                instantiatedItem.transform.rotation.x, instantiatedItem.transform.rotation.y, instantiatedItem.transform.rotation.z, instantiatedItem.transform.rotation.w,
                instantiatedItem);
        switch(item.itemType)
        {
            case ItemType.TEXTPLATE:
                {
                    TextPlateItemData textPlateItemData = new(itemData, "Sample text", true);
                    itemsData.textPlateItemsData.Add(textPlateItemData);
                    break;
                }
            case ItemType.LIGHT:
                {
                    LightItemData lightItemData = new(itemData, true);
                    itemsData.lightItemsData.Add(lightItemData);
                    break;
                }
            case ItemType.BUTTON:
                {
                    ButtonItemData buttonItemData = new(itemData, 0);
                    itemsData.buttonItemsData.Add(buttonItemData);
                    break;
                }
            case ItemType.IMAGE:
                {
                    ImageItemData imageItemData = new(itemData);
                    itemsData.imageItemsData.Add(imageItemData);
                    break;
                }
            case ItemType.AUDIO:
                {
                    AudioItemData audioItemData = new(itemData);
                    itemsData.audioItemsData.Add(audioItemData);
                    break;
                }
            default:
                {
                    itemsData.itemsData.Add(itemData);
                    break;
                }
        }
    }


    /// <summary>
    /// Move repeated code from AddItemsToScene()
    /// </summary>
    private void AddItemToScene(ItemData itemData)
    {
        Vector3 storedPosition = new Vector3(itemData.position_x, itemData.position_y, itemData.position_z);
        Quaternion storedRotation = new Quaternion(itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w);

        GameObject instantiatedItem;

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
        GameObject actionsUI = GameObject.Find("ActionsUI");
        foreach (KeyValuePair<string, Action> entry in itemData.actions)
        {
            actionsUI.GetComponent<ActionsController>().AddActionView(entry.Key);
        }
        itemData.gameObject = instantiatedItem;
    }
    public void AddItemsToScene()
    {
        foreach (ItemData itemData in itemsData.ConcatItemsData())
        {
            AddItemToScene(itemData);
        }
    }
}