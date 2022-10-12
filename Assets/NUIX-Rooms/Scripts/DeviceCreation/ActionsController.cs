using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController : MonoBehaviour
{
    public GameObject ActionsUI;
    public GameObject ActionView;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void AddActionView(string name)
    {
        GameObject actionView = Instantiate(ActionView, Vector3.zero, Quaternion.identity);
        actionView.name = name;
        actionView.transform.SetParent(ActionsUI.transform, false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
