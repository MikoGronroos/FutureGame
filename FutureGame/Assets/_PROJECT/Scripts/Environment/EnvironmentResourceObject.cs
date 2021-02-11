using UnityEngine;

public class EnvironmentResourceObject : MonoBehaviour
    , IDamageable
{

    [SerializeField] private float health;

    [SerializeField] private WeaponType weaponTypeAllowedToMakeDamage;

    public WeaponType WeaponType { get { return weaponTypeAllowedToMakeDamage; } set { } }

    public void MakeDamage(float amount)
    {
        health -= amount;
        if (CheckHealth(health))
        {
            CollectObject();
        }
    }

    private void CollectObject()
    {
        Destroy(gameObject);
    }

    private bool CheckHealth(float health)
    {
        return health <= 0;
    }
}
