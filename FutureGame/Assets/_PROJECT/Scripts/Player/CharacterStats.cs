using System;
using UnityEngine;

public class CharacterStats : Character, IDamageable
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;
    [SerializeField] private float maxThirst;
    [SerializeField] private float maxHunger;
    [SerializeField] private float maxWeight;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentStamina;
    [SerializeField] private float currentThirst;
    [SerializeField] private float currentHunger;
    [SerializeField] private float currentWeight = 0;

    [SerializeField] private bool isUsingStamina;

    [SerializeField] private WeaponType weaponTypeAllowedToMakeDamage;

    private CharacterOwner _charOwner;

    public float MaxHealth { get { return maxHealth; } private set { } }
    public float MaxStamina { get { return maxStamina; } private set { } }
    public float MaxThirst { get { return maxThirst; } private set { } }
    public float MaxHunger { get { return maxHunger; } private set { } }
    public float MaxWeight { get { return maxWeight; } private set { } }

    public float CurrentHealth { get { return currentHealth; } set
        {
            currentHealth = value;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    public float CurrentStamina { get { return currentStamina; } set
        {
            currentStamina = value;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            else if (currentStamina < 0)
            {
                currentStamina = 0;
            }
            _charOwner.PlayerUI.UpdateStaminaBar(currentStamina, maxStamina);
        }
    }

    public float CurrentThirst { get { return currentThirst; } set
        {
            currentThirst = value;
            if (currentThirst > maxThirst)
            {
                currentThirst = maxThirst;
            }
            else if (currentThirst < 0)
            {
                currentThirst = 0;
            }
            _charOwner.PlayerUI.UpdateThirstBar(currentThirst, maxThirst);
        }
    }

    public float CurrentHunger { get { return currentHunger; } set
        {
            currentHunger = value;
            if (currentHunger > maxHunger)
            {
                currentHunger = maxHunger;
            }
            else if (currentHunger < 0)
            {
                currentHunger = 0;
            }
            _charOwner.PlayerUI.UpdateHungerBar(currentHunger, maxHunger);
        }
    }

    public float CurrentWeight { get { return currentWeight; } set
        {
            currentWeight = value;
            currentWeight = (float)Math.Round(currentWeight, 2);
            _charOwner.PlayerUI.UpdateWeightCounter(currentWeight, maxWeight);
        }
    }

    public WeaponType WeaponType { get { return weaponTypeAllowedToMakeDamage; } set { } }

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentThirst = maxThirst;
        currentHunger = maxHunger;
    }

    public void MakeDamage(float amount)
    {
        currentHealth -= amount;
        _charOwner.PlayerUI.UpdateHealthBar(currentHealth, maxHealth);
    }

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

    public override Race GetCharacterRace()
    {
        return base.GetCharacterRace();
    }

}
