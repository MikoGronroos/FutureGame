using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Finark/Consumable")]
public class Consumable : Item
{

    public float healthAddon;

    public override void Use(CharacterStats player)
    {
        base.Use(player);
        player.AddHealth(healthAddon);
    }
}
