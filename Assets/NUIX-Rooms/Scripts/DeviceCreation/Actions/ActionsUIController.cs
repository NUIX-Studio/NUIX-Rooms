using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsUIController : MonoBehaviour
{


    public GameObject actionView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject AddActionView(string name)
    {
        GameObject action = Instantiate(actionView, Vector3.zero, Quaternion.identity);
        action.name = name;
        action.transform.SetParent(this.transform, false);
        return action;
    }
}
