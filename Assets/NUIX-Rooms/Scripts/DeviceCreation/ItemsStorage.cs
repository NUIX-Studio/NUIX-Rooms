using UnityEngine;


/// <summary>
/// Requires JSONSaving component, while JsonSaving requires this one. (TODO)
/// </summary>
[RequireComponent(typeof(JSONSaving))]
public class ItemsStorage : MonoBehaviour
{
    public ItemsData itemsData;

    public ItemDescription textItemDescription;
    public ItemDescription lightItemDescription;

    private void CreateItemsData()
    {
        itemsData = new ItemsData();
    }

    private void Start()
    {
        CreateItemsData();
    }

    public void AddRecord(ItemData itemData)
    {
        itemsData.itemsData.Add(itemData);

    }
    public void AddItem(GameObject item)
    {
        Instantiate(item.GetComponent<ItemDescription>().itemPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
        ItemData itemData = new ItemData(item.GetComponent<ItemDescription>().itemType,
            item.transform.position.x, item.transform.position.y, item.transform.position.z,
            item.transform.rotation.x, item.transform.rotation.y, item.transform.rotation.z);
        AddRecord(itemData);
        GetComponent<JSONSaving>().SaveData();
        AddItemsToScene();
    }

    public void AddItemsToScene()
    {
        foreach (ItemData itemData in itemsData.itemsData)
        {
            switch(itemData.itemType)
            {
                case ItemType.TEXT:
                {
                    Instantiate(textItemDescription.itemPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
                    break;
                }
                case ItemType.LIGHT:
                    {
                        Instantiate(lightItemDescription.itemPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
                        break;
                    }
                default: break;

            }

        }
    }
}