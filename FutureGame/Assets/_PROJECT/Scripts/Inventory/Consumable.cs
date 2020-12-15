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
        CharacterOwner.Instance.CharacterStats.AddHealth(healthAddon);
        CharacterOwner.Instance.CharacterStats.AddStamina(staminaAddon);
        CharacterOwner.Instance.CharacterStats.AddHunger(hungerAddon);
        CharacterOwner.Instance.CharacterStats.AddThirst(thirstAddon);
        base.Use(slot);
    }
}
