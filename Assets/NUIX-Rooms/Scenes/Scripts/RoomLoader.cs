using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public ItemPresenter itemPresenter;

    // Start is called before the first frame update
    void Start()
    {
        GameObject light = itemPresenter.CreateItem(itemPresenter.lightItemDescription);
        light.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 0.77f, 0.55f));
        GameObject button = itemPresenter.CreateItem(itemPresenter.buttonItemDescription);
        button.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 0.782f, 0.369f));
        GameObject textplate = itemPresenter.CreateItem(itemPresenter.textPlateItemDescription);
        textplate.GetComponent<ItemViewController>().SetPosition(new Vector3(0.1f, 0.782f, 0.269f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
