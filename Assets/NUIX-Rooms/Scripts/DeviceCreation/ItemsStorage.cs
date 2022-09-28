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

    private void Update()
    {
        /// Updating ItemsData every frame, so we have up-to-date data
        /// However, we may need to consider the CPU and memory load
        /// 
        foreach (ItemData itemData in itemsData.itemsData)
        {
            itemData.position_x = itemData.gameObject.transform.position.x;
            itemData.position_y = itemData.gameObject.transform.position.y;
            itemData.position_z = itemData.gameObject.transform.position.z;

            itemData.rotation_x = itemData.gameObject.transform.rotation.x;
            itemData.rotation_y = itemData.gameObject.transform.rotation.y;
            itemData.rotation_z = itemData.gameObject.transform.rotation.z;
            itemData.rotation_w = itemData.gameObject.transform.rotation.w;
        }
    }

    public void AddRecord(ItemData itemData)
    {
        itemsData.itemsData.Add(itemData);

    }
    public void AddItem(GameObject item)
    {
        GameObject instantiatedItem = Instantiate(item.GetComponent<ItemDescription>().itemPrefab, SpawningAreaPosition, Quaternion.identity);
        ItemData itemData = new ItemData(item.GetComponent<ItemDescription>().itemType,
            item.transform.position.x, item.transform.position.y, item.transform.position.z,
            item.transform.rotation.x, item.transform.rotation.y, item.transform.rotation.z, item.transform.rotation.w,
            instantiatedItem);
        AddRecord(itemData);
        //ItemsDataGameObjects.Add(instantiatedItem);
        //GetComponent<JSONSaving>().SaveData();
        //AddItemsToScene();
    }

    public void AddItemsToScene()
    {
        foreach (ItemData itemData in itemsData.itemsData)
        {
            Vector3 storedPosition = new Vector3(itemData.position_x, itemData.position_y, itemData.position_z);
            Quaternion storedRotation = new Quaternion(itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w);

            GameObject instantiatedItem;

            switch (itemData.itemType)
            {
                case ItemType.TEXTPLATE:
                    {
                        instantiatedItem = Instantiate(textPlateItemDescription.itemPrefab, storedPosition, storedRotation);
                        break;
                    }
                case ItemType.LIGHT:
                    {
                        instantiatedItem = Instantiate(lightItemDescription.itemPrefab, storedPosition, storedRotation);
                        break;
                    }
                default:
                    {
                        instantiatedItem = Instantiate(defaultItemDescription.itemPrefab, storedPosition, storedRotation);
                        break;
                    }

            }
            itemData.gameObject = instantiatedItem;
            //ItemsDataGameObjects.Add(instantiatedItem);
        }
    }
}