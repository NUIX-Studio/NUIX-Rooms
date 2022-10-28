using UnityEngine;

/// <summary>
/// Attached to a LightDescriptionItem, adding extra actions
/// </summary>
public class LightItemViewController : ItemViewController
{

    public void Start()
    {
        receiverMethods.Add(nameof(Toggle));
    }

    /// <summary>
    /// A Light component to interact with
    /// </summary>
    public Light itemLight;


    /// <summary>
    /// Receiver action to enable/disable light
    /// </summary>
    public void Toggle()
    {
        itemLight.enabled = !itemLight.enabled;
    }
}
