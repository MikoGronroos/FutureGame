using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Finark/Equipment")]
public class Equipment : Item
{

    public EquipmentType type;
    public GameObject equipmentObject;

    public override void Use(CharacterStats player)
    {
        base.Use(player);
    }

}
