using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler
{


    [SerializeField] protected Item currentItem;

    [SerializeField] protected GameObject inventoryItemObject;
    [SerializeField] protected Transform parentObject;

    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI stackSizeText;

    [SerializeField] private int currentAmountOfItems;

    public virtual int CurrentAmountOfItems
    {
        get
        {
            return currentAmountOfItems;
        }
        set
        {
            currentAmountOfItems = value;

            if (stackSizeText != null)
            {
                string stackText = $"{currentAmountOfItems.ToString()}";
                stackSizeText.text = stackText;
            }

            if (currentAmountOfItems <= 0)
            {
                currentAmountOfItems = 0;
                RefreshItem(null);
            }
        }
    }

    public virtual bool IsSlotEmpty { get { return IsEmpty(); } }

    public virtual bool IsSlotFull { get { return IsFull(); } }

    private void Awake()
    {
        parentObject = transform.parent;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {

        if (currentItem == null) { return; }

        GameObject itemInInventoryObject = Instantiate(inventoryItemObject);
        itemInInventoryObject.GetComponent<InventoryObject>().SetDragStartPos(transform.position);
        itemInInventoryObject.transform.position = transform.position;
        itemInInventoryObject.GetComponent<InventoryObject>().ThisItem = currentItem;
        itemInInventoryObject.GetComponent<InventoryObject>().SetLastSlot(this);
        itemInInventoryObject.transform.SetParent(parentObject.transform);
        eventData.pointerDrag = itemInInventoryObject;

        RemoveItem();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            if (eventData.pointerDrag.TryGetComponent(out InventoryObject invObject))
            {

                if (!IsEmpty())
                {
                    if (invObject.ThisItem.Id != currentItem.Id || IsSlotFull) return;
                }

                eventData.pointerDrag.transform.position = transform.position;

                if (IsEmpty())
                {
                    RefreshItem(invObject.ThisItem);
                }

                if (invObject.GetLastSlot() is EquipmentSlot)
                {
                    var equipmentSlot = invObject.GetLastSlot() as EquipmentSlot;
                    equipmentSlot.DequippingItem(currentItem);
                }

                CurrentAmountOfItems++;
                invObject.DroppedOnSlot = true;
                DeleteInventoryObject(eventData.pointerDrag);
            }
        }
    }

    public virtual bool RemoveItem()
    {
        CurrentAmountOfItems--;
        return true;
    }

    public virtual void AddItem(Item item)
    {
        if (CurrentAmountOfItems == 0)
        {
            RefreshItem(item);
        }
        CurrentAmountOfItems++;
    }

    private void RefreshItem(Item item)
    {

        if (item == null)
        {
            itemIcon.sprite = null;
            currentItem = null;
            return;
        }

        itemIcon.sprite = item.Icon;
        currentItem = item;
    }

    public virtual void DeleteInventoryObject(GameObject itemCarrier)
    {
        Destroy(itemCarrier);
    }

    public virtual Item GetCurrentItem()
    {
        return currentItem;
    }

    private bool IsEmpty()
    {
        return currentItem == null;
    }

    private bool IsFull()
    {
        return currentAmountOfItems >= currentItem.StackSize;
    }

}
