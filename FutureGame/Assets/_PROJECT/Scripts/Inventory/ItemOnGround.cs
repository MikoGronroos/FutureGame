using UnityEngine;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private Item itemData;

    public void Interact()
    {
        Inventory.Instance.AddItemToInventory(itemData);
        Destroy(gameObject);
    }
}
