using UnityEngine;
using System.Collections.Generic;



public class ItemsStorage : MonoBehaviour
{
    public ItemsData itemsData;


    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;
    public ItemDescription defaultItemDescription;

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
    }

    /// <summary>
    /// Instantiates an item 
    /// </summary>
    /// <param name="item"></param>
    public void CreateItem(GameObject item)
    {
        GameObject instantiatedItem = Instantiate(item.GetComponent<ItemDescription>().itemPrefab, SpawningAreaPosition, Quaternion.identity);
        ItemData itemData = new(item.GetComponent<ItemDescription>().itemType,
                item.transform.position.x, item.transform.position.y, item.transform.position.z,
                item.transform.rotation.x, item.transform.rotation.y, item.transform.rotation.z, item.transform.rotation.w,
                instantiatedItem);
        switch(item.GetComponent<ItemDescription>().itemType)
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
            default:
                {
                    instantiatedItem = Instantiate(defaultItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
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