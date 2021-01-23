using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    public int ItemID;
    public int StackSize;
    public float Weight;
    public Sprite Icon;
    public ItemType Type;

    public virtual void Use(InventorySlot slot)
    {
        CharacterOwner.Instance.Inventory.RemoveItemFromSlot(slot);
    }

}
