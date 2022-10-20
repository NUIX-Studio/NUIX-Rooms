using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemService : MonoBehaviour
{

    private ItemsData itemsData;





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

    public void SetItems(ItemsData itemsData)
    {
        this.itemsData = itemsData;
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

    public ActionData GetActionDataByID(string actionID)
    {
        foreach (ActionData actionData in itemsData.actionData)
        {
            if (actionData.actionID == actionID)
            {
                return actionData;
            }
        }
        return null;
    }

    public void AddItemData(ItemData itemData)
    {
        switch (itemData.itemType)
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

    public void AddActionData(ActionData actionData)
    {
        if (!itemsData.actionData.Contains(actionData))
        {
            itemsData.actionData.Add(actionData);
            Debug.Log("ActionData already in the list!");
        }
    }
}
