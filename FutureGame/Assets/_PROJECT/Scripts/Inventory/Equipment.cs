using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Finark/Equipment")]
public class Equipment : Item
{

    public GameObject EquipmentObject;

    public override void Use(CharacterStats player)
    {
        base.Use(player);
    }

}
