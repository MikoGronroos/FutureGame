using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Finark/Consumable")]
public class Consumable : Item
{

    public float healthAddon;
    public float staminaAddon;
    public float hungerAddon;
    public float thirstAddon;

    public override void Use(InventorySlot slot)
    {
        CharacterOwner.Instance.CharacterStats.CurrentHealth += healthAddon;
        CharacterOwner.Instance.CharacterStats.CurrentStamina += staminaAddon;
        CharacterOwner.Instance.CharacterStats.CurrentHunger += hungerAddon;
        CharacterOwner.Instance.CharacterStats.CurrentThirst += thirstAddon;
        base.Use(slot);
    }
}
