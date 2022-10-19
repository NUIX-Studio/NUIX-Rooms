using UnityEngine;

public class LightItemViewController : ItemViewController
{

    public void Start()
    {
        receiverMethods.Add(nameof(Toggle));
    }
    public Light itemLight;
    public void Toggle()
    {
        itemLight.enabled = !itemLight.enabled;
    }
}
