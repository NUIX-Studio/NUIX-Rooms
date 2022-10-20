using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionsUIViewController : MonoBehaviour
{
    public ItemPresenter itemPresenter;

    private ItemViewController currentSenderViewController;
    private int senderViewControllerIndex = 0;
    private ItemViewController currentReceiverViewController;
    private int receiverViewControllerIndex = 0;

    public TMPro.TMP_Text senderIDLabel;
    public TMPro.TMP_Text senderItemTypeLabel;
    public TMPro.TMP_Text senderMethodNameLabel;
    
    public TMPro.TMP_Text receiverIDLabel;
    public TMPro.TMP_Text receiverItemTypeLabel;
    public TMPro.TMP_Text receiverMethodNameLabel;

    public void NextSenderViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            senderViewControllerIndex++;
            if (senderViewControllerIndex == viewControllersCount)
            {
                senderViewControllerIndex = 0;
            }
            currentSenderViewController = itemPresenter.itemViewControllers.ElementAt(senderViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void NextReceiverViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            receiverViewControllerIndex++;
            if (receiverViewControllerIndex == viewControllersCount)
            {
                receiverViewControllerIndex = 0;
            }
            currentReceiverViewController = itemPresenter.itemViewControllers.ElementAt(receiverViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    private void UpdateView()
    {
        senderIDLabel.text = currentSenderViewController.itemID;
        senderItemTypeLabel.text = itemPresenter.GetItemDatabyID(currentSenderViewController.itemID).itemType.ToString();
        
        receiverIDLabel.text = currentReceiverViewController.itemID;
        receiverItemTypeLabel.text = itemPresenter.GetItemDatabyID(currentReceiverViewController.itemID).itemType.ToString();

        // Update methods based on viewcontrollers
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
