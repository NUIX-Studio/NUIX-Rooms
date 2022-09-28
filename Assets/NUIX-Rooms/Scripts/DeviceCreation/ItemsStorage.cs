using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Requires JSONSaving component, while JsonSaving requires this one. (TODO)
/// </summary>
[RequireComponent(typeof(JSONSaving))]
public class ItemsStorage : MonoBehaviour
{
    public ItemsData itemsData;

    public List<GameObject> ItemsDataGameObjects;

    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;

    public Vector3 SpawningAreaPosition;

    private void CreateItemsData()
    {
        itemsData = new ItemsData();
    }

    private void Start()
    {
        CreateItemsData();
    }

    private void Update()
    {
        /// Updating ItemsData every frame, so we have up-to-date data
        /// However, we may need to consider the CPU and memory load
        /// 
        for (int i = 0; i < itemsData.itemsData.Count; i++)
        {
            itemsData.itemsData[i].position_x = ItemsDataGameObjects[i].transform.position.x;
            itemsData.itemsData[i].position_y = ItemsDataGameObjects[i].transform.position.y;
            itemsData.itemsData[i].position_z = ItemsDataGameObjects[i].transform.position.z;

            itemsData.itemsData[i].rotation_x = ItemsDataGameObjects[i].transform.rotation.x;
            itemsData.itemsData[i].rotation_y = ItemsDataGameObjects[i].transform.rotation.y;
            itemsData.itemsData[i].rotation_z = ItemsDataGameObjects[i].transform.rotation.z;
            itemsData.itemsData[i].rotation_w = ItemsDataGameObjects[i].transform.rotation.w;
        }
    }

    public void AddRecord(ItemData itemData)
    {
        itemsData.itemsData.Add(itemData);

    }
    public void AddItem(GameObject item)
    {
        GameObject InstantiatedItem = Instantiate(item.GetComponent<ItemDescription>().itemPrefab, SpawningAreaPosition, Quaternion.identity);
        ItemData itemData = new ItemData(item.GetComponent<ItemDescription>().itemType,
            item.transform.position.x, item.transform.position.y, item.transform.position.z,
            item.transform.rotation.x, item.transform.rotation.y, item.transform.rotation.z, item.transform.rotation.w);
        AddRecord(itemData);
        ItemsDataGameObjects.Add(InstantiatedItem);
        //GetComponent<JSONSaving>().SaveData();
        //AddItemsToScene();
    }

    public void AddItemsToScene()
    {
        foreach (ItemData itemData in itemsData.itemsData)
        {
            Vector3 storedPosition = new Vector3(itemData.position_x, itemData.position_y, itemData.position_z);
            Quaternion storedRotation = new Quaternion(itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w);

            switch (itemData.itemType)
            {
                case ItemType.TEXTPLATE:
                {
                    Instantiate(textPlateItemDescription.itemPrefab, storedPosition, storedRotation);
                    break;
                }
                case ItemType.LIGHT:
                    {
                        Instantiate(lightItemDescription.itemPrefab, storedPosition, storedRotation);
                        break;
                    }
                default: break;

            }

        }
    }
}