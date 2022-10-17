using UnityEngine;



/// <summary>
/// Responsible for updating the Item GameObject
/// And tracking its changes
/// </summary>
public class ItemViewController : MonoBehaviour
{
    private ItemPresenter itemPresenter;

    private GameObject itemGameObject;

    public Vector3 SpawningAreaPosition;

    public string itemID;


    void Start()
    {
        if (GetComponent<ItemPresenter>() != null)
        {
            itemPresenter = GetComponent<ItemPresenter>();
        }
    }


    private void UpdateView()
    {

    }

    public Transform GetItemTransform()
    {
        return gameObject.transform;
    }

    public void SetItemTransform(Transform itemTransform)
    {
        if (itemGameObject != null)
        {
            itemGameObject.transform.position = itemTransform.position;
            itemGameObject.transform.rotation = itemTransform.rotation;
            itemGameObject.transform.localScale = itemTransform.localScale;
        }
    }
}
