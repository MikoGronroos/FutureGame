using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;
    [SerializeField] private float maxThirst;
    [SerializeField] private float maxHunger;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentStamina;
    [SerializeField] private float currentThirst;
    [SerializeField] private float currentHunger;

    [SerializeField] private bool isUsingStamina;

    [SerializeField] private WeaponType weaponTypeAllowedToMakeDamage;

    private CharacterOwner _charOwner;

    public float MaxHealth { get { return maxHealth; } private set { } }
    public float MaxStamina { get { return maxStamina; } private set { } }
    public float MaxThirst { get { return maxThirst; } private set { } }
    public float MaxHunger { get { return maxHunger; } private set { } }

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }
    public float CurrentThirst { get { return currentThirst; } set { currentThirst = value; } }
    public float CurrentHunger { get { return currentHunger; } set { currentHunger = value; } }

    public WeaponType WeaponType { get { return weaponTypeAllowedToMakeDamage; } set { } }

    private void Awake()
    {
        _charOwner = CharacterOwner.Instance;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentThirst = maxThirst;
        currentHunger = maxHunger;
    }

    public void MakeDamage(float amount)
    {
        Debug.Log($"");
        currentHealth -= amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    #region StatAddonAndReduction

    public void AddHealth(float amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            amount = amount - (currentHealth + amount - maxHealth);
        }
        currentHealth += amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ReduceHealth(float amount)
    {

        currentHealth -= amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ReduceStamina(float amount)
    {
        isUsingStamina = true;
        currentStamina -= amount;
        _charOwner.PlayerUI.UpdateStaminaBar(currentStamina, maxStamina);
    }

    public void AddStamina(float amount)
    {
        if (currentStamina + amount > maxStamina)
        {
            amount = amount - (currentStamina + amount - maxStamina);
        }
        currentStamina += amount;
        _charOwner.PlayerUI.UpdateStaminaBar(currentStamina, maxStamina);
    }

    public void ReduceHunger(float amount)
    {
        currentHunger -= amount;
        _charOwner.PlayerUI.UpdateHungerBar(currentHunger, maxHunger);
    }

    public void AddHunger(float amount)
    {
        currentHunger += amount;
        _charOwner.PlayerUI.UpdateHungerBar(currentHunger, maxHunger);
    }

    public void ReduceThirst(float amount)
    {
        currentThirst -= amount;
        _charOwner.PlayerUI.UpdateThirstBar(currentThirst, maxThirst);
    }

    public void AddThirst(float amount)
    {
        currentThirst += amount;
        _charOwner.PlayerUI.UpdateThirstBar(currentThirst, maxThirst);
    }

    #endregion

    private void OnValidate()
    {

        #region Stat clamping

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
        if (currentThirst < 0)
        {
            currentThirst = 0;
        }
        if (currentHunger < 0)
        {
            currentHunger = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }

        if (currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
        }

        #endregion

    }

}
