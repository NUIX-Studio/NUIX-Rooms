using UnityEngine;



/// <summary>
/// Responsible for updating the Item GameObject
/// And tracking its changes
/// </summary>
[RequireComponent(typeof(ItemPresenter))]
public class ItemViewController : MonoBehaviour
{
    private ItemPresenter itemPresenter;

    private GameObject itemGameObject;

    public Vector3 SpawningAreaPosition;

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
        if (itemGameObject != null)
        {
            return itemGameObject.transform;
        }
        else
        {
            return null;
        }
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

    public GameObject CreateItemGameObject(ItemDescription item)
    {
        GameObject instantiatedItem = Instantiate(item.itemPrefab, SpawningAreaPosition, Quaternion.identity);

    }

}