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
    private int seceiverViewControllerIndex = 0;

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
                currentSenderViewController = itemPresenter.itemViewControllers.ElementAt(senderViewControllerIndex).Value;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
        

    }

    private void UpdateView()
    {
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
