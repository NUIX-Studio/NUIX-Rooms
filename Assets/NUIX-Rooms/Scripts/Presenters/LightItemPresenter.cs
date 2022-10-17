using UnityEngine;

public class LightItemPresenter : ItemPresenter
{
    public Light lightGameObject;


    /// <summary>
    /// Toggle the Light both in Model and View Layers
    /// </summary>
    //public void Toggle()
    //{
    //    ItemsStorage it = GameObject.Find("NUIXBus").GetComponent<ItemsStorage>();
    //    Debug.Log("Light toggled");
    //    ((LightItemData) GameObject.Find("NUIXBus").GetComponent<ItemsStorage>().
    //        GetItemDataByID(itemID)).isTurnedON = 
    //        !((LightItemData) GameObject.Find("NUIXBus").GetComponent<ItemsStorage>().
    //        GetItemDataByID(itemID)).isTurnedON;

    //    lightGameObject.enabled = !lightGameObject.enabled;
    //}

    //public LightItemPresenter()
    //{
    //    actions.AddListener(Toggle);
    //}
}
