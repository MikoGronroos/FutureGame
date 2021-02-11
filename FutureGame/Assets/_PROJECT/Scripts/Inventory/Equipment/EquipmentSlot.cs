using UnityEngine.EventSystems;
using UnityEngine;

public class EquipmentSlot : Slot, IDropHandler, IBeginDragHandler, IDragHandler
{

    [SerializeField] private EquipmentType acceptedEquipmentType;

    private EquippingItem _equippingItem;

    private void Awake()
    {
        _equippingItem = FindObjectOfType<EquippingItem>();
    }

    public override bool IsSlotEmpty => base.IsSlotEmpty;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {

        var equipment = eventData.pointerDrag.GetComponent<InventoryObject>().ThisItem as EquipmentItem;
        if (equipment.ThisEquipmentType != acceptedEquipmentType) return;

        base.OnDrop(eventData);

        //If this is before base.OnDrop currentItem is null
        _equippingItem.EquipItem(currentItem);
    }

    public override void RefreshItem(Item item)
    {
        base.RefreshItem(item);
    }

    public override void DeleteInventoryObject(GameObject itemCarrier)
    {
        base.DeleteInventoryObject(itemCarrier);
    }

}
