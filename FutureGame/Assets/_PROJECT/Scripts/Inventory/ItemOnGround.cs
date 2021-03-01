using UnityEngine;
using UnityEngine.Events;

public class ItemOnGround : MonoBehaviour, IInteractable
{

    [SerializeField] private Item itemData;
    [SerializeField] private int Amount = 1;

    [SerializeField] private UnityEvent OnItemAddedToInventoryEvent;

    public void Interact()
    {
        Inventory.Instance.AddToInventory(itemData, Amount);
        OnItemAddedToInventoryEvent?.Invoke();
        Destroy(gameObject);
    }
}
