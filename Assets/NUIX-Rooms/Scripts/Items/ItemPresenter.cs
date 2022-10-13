using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsible for sending the actions to connected GameObjects
/// Also implements the actions themselves
/// </summary>
public class ItemPresenter : MonoBehaviour
{
    public string itemID;
    public UnityEvent actions = new();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
