using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Inventory/Resource")]
public class ResourceItem : Item
{
    public override ItemType Type { get => ItemType.Resource; }
}
