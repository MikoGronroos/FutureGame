using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Inventory/Consumable")]
public class ConsumableItem : Item
{

    public override ItemType Type { get => ItemType.Consumable;}

}
