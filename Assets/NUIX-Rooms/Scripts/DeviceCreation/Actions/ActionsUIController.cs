using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsUIController : MonoBehaviour
{
    public ItemsStorage itemsStorage;
    public GameObject actionView;
    public GameObject actionsLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TrackActionsAvailability(ActionData current_actionData, bool isOn)
    {
        foreach(ActionData actionData in itemsStorage.itemsData.actionData)
        {
            if (actionData.fromItemID == current_actionData.fromItemID &&
                actionData.fromItemMethodName == current_actionData.fromItemMethodName &&
                actionData.toItemID == current_actionData.toItemID &&
                actionData.toItemMethodName == current_actionData.toItemMethodName)
            {
                actionData.isActionEnabled = isOn;
            }
        }
    }


    public GameObject AddActionView(ActionData actionData)
    {
        GameObject action = Instantiate(actionView, Vector3.zero, Quaternion.identity);
        action.name = actionData.ToString();
        action.transform.SetParent(actionsLabel.transform, false);
        action.GetComponentInChildren<TMPro.TMP_Text>().text = action.name;
        action.GetComponentInChildren<Toggle>().isOn = actionData.isActionEnabled;
        action.GetComponentInChildren<Toggle>().onValueChanged.AddListener(
            delegate
            {
                TrackActionsAvailability(actionData, action.GetComponentInChildren<Toggle>().isOn);
            });
        return action;
    }
}
