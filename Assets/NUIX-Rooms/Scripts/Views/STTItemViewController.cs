using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STTItemViewController : ItemViewController
{

    private Dictionary<string, Action> phrases;

    public event Action WhenSelected = delegate { };

    public SpeechRecognition speechRecognition;


    [SerializeField] private ActiveStateSelector[] _phrases;    // Start is called before the first frame update
    void Start()
    {
        if (phrases == null) phrases = new Dictionary<string, Action>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AnalyzeSpeechRecognized(string result)
    {
        //char[] separators = new char[] { ' ', '.' };

        //foreach (var word in result.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        foreach (KeyValuePair<string, Action> phrase in phrases)
        {
            if (result == phrase.Key)
            {
                phrase.Value?.Invoke();
            }
        }
    }

    public void AddPhraseFromRecognized()
    {
        AddPhrase(speechRecognition.RecognizedString());
    }

    private void AddPhrase(string phrase)
    {
        ActionData emptyAction = new ActionData();
        emptyAction.senderMethod = phrase;
        emptyAction.senderID = itemID;
        CreateNewOrUpdateExistingSenderMethod(emptyAction);
        phrases.Add(phrase, delegate { });
        phrases[phrase] += () => CallReceiverMethod(phrase);
    }

    /*
    void AddTextToSTTAction(string text)
    {
        ActionData emptyAction = new ActionData();
        emptyAction.senderMethod = _poses[poseNumber].gameObject.name;
        emptyAction.senderID = itemID;
        CreateNewOrUpdateExistingSenderMethod(emptyAction);
    }

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
    */
}
