using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemService : MonoBehaviour
{

    private ItemsData itemsData;


    public ItemDescription defaultItemDescription;
    public ItemDescription textPlateItemDescription;
    public ItemDescription lightItemDescription;
    public ItemDescription buttonItemDescription;
    public ItemDescription imageItemDescription;
    public ItemDescription audioItemDescription;


    // Start is called before the first frame update
    void Start()
    {
        if (itemsData == null)
        {
            itemsData = new ItemsData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public ItemsData GetItems()
    {
        return itemsData;
    }


    public ItemData GetItemDataByID(string itemID)
    {
        foreach (ItemData itemData in itemsData.ConcatItemsData())
        {
            if (itemData.itemID == itemID)
            {
                return itemData;
            }
        }
        return null;
    }
}
