using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IDropHandler, IBeginDragHandler, IDragHandler
{

    public override bool IsSlotEmpty => base.IsSlotEmpty;

    public override bool IsSlotFull => base.IsSlotFull;

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
        base.OnDrop(eventData);
    }

    public override void AddItem(Item item)
    {
        base.AddItem(item);
    }

    public override void DeleteInventoryObject(GameObject itemCarrier)
    {
        base.DeleteInventoryObject(itemCarrier);
    }

    public override Item GetCurrentItem()
    {
        return base.GetCurrentItem();
    }

    public override bool RemoveItem()
    {
        return base.RemoveItem();
    }

}
