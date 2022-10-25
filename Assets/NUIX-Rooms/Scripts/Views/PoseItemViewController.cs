using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine.Assertions;

public class PoseItemViewController : ItemViewController
{

    [SerializeField] private ActiveStateSelector[] _poses;
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("HandRefLeft").gameObject.GetComponent<HandRef>().InjectHand(GameObject.Find("NUIXHandRefLeft").GetComponent<HandRef>().Hand);
        transform.Find("HandRefRight").gameObject.GetComponent<HandRef>().InjectHand(GameObject.Find("NUIXHandRefRight").GetComponent<HandRef>().Hand);
        for (int i = 0; i < _poses.Length; i++)
        {
            int poseNumber = i;
            //_poses[i].WhenSelected += () => ShowVisuals(poseNumber);
            //_poses[i].WhenUnselected += () => HideVisuals(poseNumber);

            _poses[i].WhenSelected += () => CallReceiverMethod(_poses[poseNumber].gameObject.name);
            ActionData emptyAction = new ActionData();
            emptyAction.senderMethod = _poses[poseNumber].gameObject.name;
            emptyAction.senderID = itemID;
            CreateNewOrUpdateExistingSenderMethod(emptyAction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
