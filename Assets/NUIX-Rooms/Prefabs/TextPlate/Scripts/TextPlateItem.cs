using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPlateItem : MonoBehaviour
{
    public GameObject keyboard;

    public TMPro.TMP_Text plateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEnableKeyboard()
    {
        if (keyboard != null)
        {
            keyboard.SetActive(!keyboard.activeInHierarchy);
        }
    }
}
