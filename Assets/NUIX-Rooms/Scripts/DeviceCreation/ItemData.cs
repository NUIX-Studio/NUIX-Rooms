using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public ItemType itemType;

    public ItemData(ItemType itemType = ItemType.DEFAULT, 
        float position_x = 0f, float position_y = 0f, float position_z = 0f, 
        float rotation_x = 0f, float rotation_y = 0f, float rotation_z = 0f)
    {
        this.position_x = position_x;
        this.position_y = position_y;
        this.position_z = position_z;
        this.rotation_x = rotation_x;
        this.rotation_y = rotation_y;
        this.rotation_z = rotation_z;
        this.itemType = itemType;
    }

    public override string ToString()
    {
        return $"Item of type {itemType} at position x = {position_x}, y = {position_y}, z = {position_z}, " +
            $"rotation x = {rotation_x}, y = {rotation_y}, z = {rotation_z}";
    }
}

[System.Serializable]
public class ItemsData
{
    public List<ItemData> itemsData;

    public ItemsData()
    {
        this.itemsData = new List<ItemData>();
    }

    public override string ToString()
    {
        string res = "";
        foreach (ItemData itemData in itemsData)
        {
            res += $"Item of type {itemData.itemType} at position x = {itemData.position_x}, " +
                $"y = {itemData.position_y}, z = {itemData.position_z}, " +
            $"rotation x = {itemData.rotation_x}, y = {itemData.rotation_y}, " +
            $"z = {itemData.rotation_z}";
        }
        return res;
    }
}