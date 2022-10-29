using System;
using System.Collections.Generic;
using UnityEngine;

public class WeightScalerItemViewController : ItemViewController
{

    public TMPro.TMP_Text currentWeightLabel;
    public TMPro.TMP_Text requiredWeightLabel;

    float requiredWeight = 1.0f;
    public WeightScalerPlaneCollisionController weightScalerPlaneCollisionController;

    // Start is called before the first frame update
    void Start()
    {
        ActionData emptyAction = new ActionData();
        emptyAction.senderMethod = nameof(WeightTrigger);
        emptyAction.senderID = itemID;
        CreateNewOrUpdateExistingSenderMethod(emptyAction);
    }
    
    /// <summary>
    /// A method to be called when the total weight on the weighscaler exceeds the value set
    /// </summary>
    public void WeightTrigger()
    {
        Debug.Log($"Total weight {weightScalerPlaneCollisionController.totalWeight} exceeded the weight set {requiredWeight}");
    }

    public void IncreaseRequiredWeight()
    {
        requiredWeight += 1;
    }

    public void DecreaseRequiredWeight()
    {
        requiredWeight -= 1;
    }

    private void UpdateTextLabels()
    {
        currentWeightLabel.text = "Current Weight:" + Environment.NewLine + weightScalerPlaneCollisionController.totalWeight.ToString();
        requiredWeightLabel.text = "Trigger Weight:" + Environment.NewLine + requiredWeight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float currentWeight = weightScalerPlaneCollisionController.totalWeight;

        if (currentWeight > requiredWeight)
        {
            WeightTrigger();
        }
        UpdateTextLabels();
    }
}