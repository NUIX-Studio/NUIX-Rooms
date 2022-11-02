using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public ItemPresenter itemPresenter;

    [SerializeField] public bool itemsToCreate1;
    [SerializeField] public bool itemsToCreate2;
    [SerializeField] public bool itemsToCreate3;
    [SerializeField] public bool itemsToCreate4;
    [SerializeField] public bool itemsToCreate5;

    // Start is called before the first frame update
    void Start()
    {
        if (itemsToCreate1) CreateItems1();    
        if (itemsToCreate2) CreateItems2();    
    }

    public void CreateItems1()
    {
        GameObject light = itemPresenter.CreateItem(itemPresenter.lightItemDescription);
        light.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 0.77f, 0.55f));
        GameObject button = itemPresenter.CreateItem(itemPresenter.buttonItemDescription);
        button.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 0.782f, 0.369f));
        GameObject textplate = itemPresenter.CreateItem(itemPresenter.textPlateItemDescription);
        textplate.GetComponent<ItemViewController>().SetPosition(new Vector3(0.1f, 0.782f, 0.269f));
    }

    private void CreateItems2()
    {
        GameObject pose = itemPresenter.CreateItem(itemPresenter.poseItemDescription);
        pose.GetComponent<ItemViewController>().SetPosition(new Vector3(-0.1f, 0.8f, 0.2f));
        GameObject image = itemPresenter.CreateItem(itemPresenter.imageItemDescription);
        image.GetComponent<ItemViewController>().SetPosition(new Vector3(0f, 0.785f, 0.2f));
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
