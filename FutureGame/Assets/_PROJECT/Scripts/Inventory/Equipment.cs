using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Finark/Equipment")]
public class Equipment : Item
{

    public GameObject EquipmentObject;
    public EquipmentType EquipmentType;
    public bool isEquipped = false;

    public override void Use(InventorySlot slot)
    {
        if (!isEquipped)
        {
            base.Use(slot);
            CharacterOwner.Instance.Inventory.Equip(this);
        }
        else
        {
            CharacterOwner.Instance.Inventory.Dequip(this);
        }
    }
}