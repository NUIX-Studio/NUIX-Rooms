using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public float rotation_w;
    public ItemType itemType;
    [System.NonSerialized] public GameObject gameObject;

    public ItemData(ItemType itemType = ItemType.DEFAULT, 
        float position_x = 0f, float position_y = 0f, float position_z = 0f, 
        float rotation_x = 0f, float rotation_y = 0f, float rotation_z = 0f, float rotation_w = 0f,
        GameObject gameObject = null)
    {
        this.position_x = position_x;
        this.position_y = position_y;
        this.position_z = position_z;
        this.rotation_x = rotation_x;
        this.rotation_y = rotation_y;
        this.rotation_z = rotation_z;
        this.rotation_w = rotation_w;
        this.itemType = itemType;
        this.gameObject = gameObject;
    }

    public override string ToString()
    {
        return $"Item of type {itemType} at position x = {position_x}, y = {position_y}, z = {position_z}, " +
            $"rotation x = {rotation_x}, y = {rotation_y}, z = {rotation_z}, w = {rotation_w}";
    }
}

[System.Serializable]
public class TextPlateItemData : ItemData
{
    public string text = "";
    public bool isKeyboardOpen = true;

    public TextPlateItemData(ItemType itemType,
    float position_x, float position_y, float position_z,
    float rotation_x, float rotation_y, float rotation_z, float rotation_w,
    GameObject gameObject, string text, bool isKeyboardOpen) : base (itemType, position_x, position_y, position_z,
    rotation_x, rotation_y, rotation_z, rotation_w, gameObject)
    {
        this.text = text;
        this.isKeyboardOpen = isKeyboardOpen;
    }
    public TextPlateItemData(ItemData itemData, string text, bool isKeyboardOpen) 
        : base (itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w, 
    itemData.gameObject)
    {
        this.text = text;
        this.isKeyboardOpen = isKeyboardOpen;
    }
    public override string ToString()
    {
        return $"Item of type {itemType} at position x = {position_x}, y = {position_y}, z = {position_z}, " +
            $"rotation x = {rotation_x}, y = {rotation_y}, z = {rotation_z}, w = {rotation_w}," +
            $"text = {text}, keyboard is open = {isKeyboardOpen}";
    }
}

[System.Serializable]
public class LightItemData : ItemData
{
    public bool isTurnedON;

    public LightItemData(ItemData itemData, bool isTurnedON)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w,
    itemData.gameObject)
    {
        this.isTurnedON = isTurnedON;
    }
    public override string ToString()
    {
        return $"Item of type {itemType} at position x = {position_x}, y = {position_y}, z = {position_z}, " +
            $"rotation x = {rotation_x}, y = {rotation_y}, z = {rotation_z}, w = {rotation_w}," +
            $"light is on : {isTurnedON}";
    }
}


[System.Serializable]
public class ButtonItemData : ItemData
{
    // TODO: change to enum
    public int type;

    public ButtonItemData(ItemData itemData, int type)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w,
    itemData.gameObject)
    {
        this.type = type;
    }
    public override string ToString()
    {
        return $"Item of type {itemType} at position x = {position_x}, y = {position_y}, z = {position_z}, " +
            $"rotation x = {rotation_x}, y = {rotation_y}, z = {rotation_z}, w = {rotation_w}," +
            $"type : {type}";
    }
}


[System.Serializable]
public class ItemsData
{
    public List<ItemData> itemsData;

    public List<TextPlateItemData> textPlateItemsData;

    public List<LightItemData> lightItemsData;


    public IEnumerable<ItemData> ConcatItemsData()
    {
        return itemsData.Concat(textPlateItemsData).Concat(lightItemsData);
    }

    public ItemsData()
    {
        this.itemsData = new List<ItemData>();
        this.textPlateItemsData = new List<TextPlateItemData>();
        this.lightItemsData = new List<LightItemData>();
    }

    public override string ToString()
    {
        string res = "";
        foreach (ItemData itemData in ConcatItemsData())
        {
            res += itemData.ToString();
        }
        return res;
    }
}