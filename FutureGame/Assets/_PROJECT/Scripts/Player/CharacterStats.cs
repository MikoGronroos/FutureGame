using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentStamina;

    private CharacterOwner _charOwner;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void MakeDamage(float amount)
    {
        currentHealth -= amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void AddHealth(float amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            amount = amount - (currentHealth + amount - maxHealth);
        }
        currentHealth += amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        _charOwner.PlayerUI.UpdateStaminaBar(currentStamina, maxStamina);
    }

}
