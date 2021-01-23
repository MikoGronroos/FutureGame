using UnityEngine;

public class PlayerVitalsManager : MonoBehaviour
{

    [SerializeField] private float hungerReduction;
    [SerializeField] private float thirstReduction;

    [SerializeField] private float healthReductionWithoutFood;
    [SerializeField] private float healthReductionWithoutDrink;

    [SerializeField] private float healthReduction;
    [SerializeField] private bool isDying;
    [SerializeField] private bool hasThirst = true;
    [SerializeField] private bool hasHunger = true;

    private CharacterOwner _charOwner;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
    }

    private void Update()
    {
        UpdateVitals();
    }

    private void UpdateVitals()
    {

        if (_charOwner.CharacterStats.CurrentHunger <= 0)
        {
            if (hasHunger)
            {
                healthReduction += healthReductionWithoutFood;
            }
            hasHunger = false;
        }
        else
        {
            if (!hasHunger)
            {
                healthReduction -= healthReductionWithoutFood;
                hasHunger = true;
            }
            _charOwner.CharacterStats.CurrentHunger -= hungerReduction * Time.deltaTime;
        }

        if (_charOwner.CharacterStats.CurrentThirst <= 0)
        {
            if (hasThirst)
            {
                healthReduction += healthReductionWithoutDrink;
            }
            hasThirst = false;
        }
        else
        {
            if (!hasThirst)
            {
                healthReduction -= healthReductionWithoutDrink;
                hasThirst = true;
            }
            _charOwner.CharacterStats.CurrentThirst -= thirstReduction * Time.deltaTime;
        }

        if (!hasThirst || !hasHunger)
        {
            _charOwner.CharacterStats.CurrentHealth -= healthReduction * Time.deltaTime;
            isDying = true;
        }
        else
        {
            if (isDying)
            {
                isDying = false;
            }
        }

    }
}
