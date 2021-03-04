using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Inventory/Equipment")]
public class EquipmentItem : Item
{
    public override ItemType Type { get => ItemType.Equipment; }

    public EquipmentType ThisEquipmentType;
    public GameObject ItemObject;
    public AnimatorOverrideController ThisAnimatorOverrideController;

    [Header("Weapon Stats")]

    public float Damage;

}
