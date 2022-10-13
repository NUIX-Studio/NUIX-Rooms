using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public float rotation_w;
    public ItemType itemType;

    // TODO: change to list
    public ActionData actionData;


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

        this.id = Guid.NewGuid().ToString();
    }


    public override string ToString()
    {
        return $"Item of type {itemType} at " +
            $"position x = {string.Format("{0:0.00}", position_x)}, " +
            $"y = {string.Format("{0:0.00}", position_y)}, " +
            $"z = {string.Format("{0:0.00}", position_z)}, " +
            $"rotation x = {string.Format("{0:0.00}", rotation_x)}, " +
            $"y = {string.Format("{0:0.00}", rotation_y)}, " +
            $"z = {string.Format("{0:0.00}", rotation_z)}, " +
            $"w = {string.Format("{0:0.00}", rotation_w)}";
    }


    public void AddAction(Action actionMethod, string toItemID)
    {
        actionData.itemID = this.id;
        actionData.toItemID = toItemID;
        actionData.methodname = nameof(actionMethod);
        actionData.action = actionMethod;
    }

}

[System.Serializable]
public class TextPlateItemData : ItemData
{
    public string text = "";
    public bool isKeyboardOpen = true;

    public void ToggleKeyboardVisibility()
    {
        isKeyboardOpen = !isKeyboardOpen;
    }

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
        //actions.Add(nameof(ToggleKeyboardVisibility), ToggleKeyboardVisibility);
    }
    public override string ToString()
    {
        return base.ToString() + $" text = {text}, keyboard is open = {isKeyboardOpen}";
    }
}

[System.Serializable]
public class LightItemData : ItemData
{
    public bool isTurnedON;


    public void Toggle()
    {
        isTurnedON = !isTurnedON;
    }

    public LightItemData(ItemData itemData, bool isTurnedON)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w,
    itemData.gameObject)
    {
        this.isTurnedON = isTurnedON;
        //actions.Add(nameof(this.Toggle), Toggle);
    }

    public LightItemData()
    {
        //actions = new Dictionary<string, Action>();
        //actions.Add(nameof(this.Toggle), Toggle);
    }
    public override string ToString()
    {
        return base.ToString() + $" light is on : {isTurnedON}, method: {nameof(this.Toggle)}";
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
        return base.ToString() + $" type : {type}";
    }
}

[System.Serializable]
public class ImageItemData : ItemData
{

    public ImageItemData(ItemData itemData)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w,
    itemData.gameObject)
    {
        
    }
    public override string ToString()
    {
        return base.ToString();
    }
}

[System.Serializable]
public class AudioItemData : ItemData
{

    public AudioItemData(ItemData itemData)
        : base(itemData.itemType, itemData.position_x, itemData.position_y, itemData.position_z,
    itemData.rotation_x, itemData.rotation_y, itemData.rotation_z, itemData.rotation_w,
    itemData.gameObject)
    {
        
    }
    public override string ToString()
    {
        return base.ToString();
    }
}


[System.Serializable]
public class ItemsData
{
    public List<ItemData> itemsData;

    public List<TextPlateItemData> textPlateItemsData;

    public List<LightItemData> lightItemsData;

    public List<ButtonItemData> buttonItemsData;

    public List<ImageItemData> imageItemsData;

    public List<AudioItemData> audioItemsData;


    public IEnumerable<ItemData> ConcatItemsData()
    {
        return itemsData.Concat(textPlateItemsData).
            Concat(lightItemsData).
            Concat(buttonItemsData).
            Concat(imageItemsData).
            Concat(audioItemsData);
    }

    public ItemsData()
    {
        this.itemsData = new List<ItemData>();
        this.textPlateItemsData = new List<TextPlateItemData>();
        this.lightItemsData = new List<LightItemData>();
        this.buttonItemsData = new List<ButtonItemData>();
        this.imageItemsData = new List<ImageItemData>();
        this.audioItemsData = new List<AudioItemData>();
    }

    public override string ToString()
    {
        string res = "";
        foreach (ItemData itemData in ConcatItemsData())
        {
            res += itemData.ToString() + Environment.NewLine;
        }
        return res;
    }
}



public class ActionData
{
    public string itemID;

    public string toItemID;

    public string methodname;

    //[System.NonSerialized] public Dictionary<string, Action> actions;
    [System.NonSerialized] public Action action;

}